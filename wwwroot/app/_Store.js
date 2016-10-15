import { createStore, applyMiddleware } from 'redux';
import { debounce } from 'underscore';
import thunk from 'redux-thunk';

import { storeStateInLS,getStateFromLS, get, fetchPut, fetchPost, uuid } from './util';
import reducer from './reducers/_Reducer';

// middleware - the functions that intercept an action upon dispatch, before it reaches the reducer
// applyMiddleware - the function that helps you make your replacement for createStore;
// thunk function -  a function that wraps some behavior for execution (e.g. logger)... it's
// let store = applyMiddleware(thunk)(createStore)(reducer);
/*  e.g. let createStoreMW = applyMiddleware(logger, crashReporter)(createStore);
*   todoApp = combineReducers(reducers)
*   let store= createStoreMW(todoApp)

*/


/* Working Post Request: 
     {
         "id": "dd2af63d-1243-4e25-bdcc-d6dcff5ce202", // needs to be unique
         "order": 2,
         "paused": true,
         "time": 20,
         "ticking": false,
         "title": "pray",
         "timerGroupId": "35735752-bbe1-43dc-87d5-1deec4e61d09",
         "startTime": 20
       }
     }
   */


export const defaultTimer = (order = 1, id = uuid(), timerGroupId = 'f7492212-1e19-4a15-8e5c-1c2652f51d00') => ({
  order,
  id,
  timerGroupId,  //this guid changes on every seed
  title: 'DEFAULTSTATE',
  ticking: false,
  time: 5,
  startTime: 5,
  paused: true
})


const getStateFromDB = () => get('/api/timer');

const getInitialState = () => {
  return Promise.all([getStateFromDB(), getStateFromLS()]).then(([dbState, lsState]) => {
    console.log('dbState', dbState);
    if (dbState) return dbState;
      console.warn('lsState returned.')
    if (lsState) return lsState;
      console.warn('defaultState returned.')
    return [...defaultTimer];
  });
};



const compose = ((a, b) => (c) => a(b(c)));

const configureStore = () => {
  const mw = compose(
      applyMiddleware(thunk),
      window.devToolsExtension ? window.devToolsExtension() : f => f
    );
    return createStore(reducer, mw);
  };

const store = configureStore();


     /* this will only work if postman works... I should be able to add this in the body
        /*	{
          "title": "POSTED!",
          "time": 666,
          "ticking": false,
          "startTime": 20,
          "paused": true,
          "order": 5,
          "timerGroupId": "80d7131d-a862-4fd1-958d-3b8ac62980ba"
          }
        */

// without anon function, I get error 'expected listener to be a function'
store.subscribe(() => storeStateInLS(store.getState()));

// this seems to be called everytime the state changes
const debouncedUpdate = debounce(() => { 
  getStateFromLS().then(currentState => {
      console.log('currentState', currentState);
      // for each new timer, do a post
      currentState.map(timer =>
      // if existing, do a put 
      fetchPut('/api/timer', timer)
   
        .then((res) => console.log('debounced update!', res))
  )}, 3000);
})

store.subscribe(debouncedUpdate);

// this should probably go in ActionCreators, but it didn't work in their for some reason'
const hydrate = (data) =>  {
    store.dispatch({type: 'HYDRATE', data});
    store.dispatch({type: 'RESET_ALL'});
    // pause existing timers
    store.dispatch({type: 'PAUSE_ALL'});
    store.dispatch({type: 'SET_TICKING_FALSE_ALL'});
};

getInitialState().then((state) => {
  console.log('getInitialState', state);
  hydrate(state);
})


export default store;





