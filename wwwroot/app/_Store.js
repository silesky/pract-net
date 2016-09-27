import { createStore, applyMiddleware } from 'redux';
import { debounce } from 'underscore';
import { storeStateInLS,getStateFromLS, get, fetchPut, fetchPost } from './util';
import thunk from 'redux-thunk';
import reducer from './reducers/_Reducer';

// middleware - the functions that intercept an action upon dispatch, before it reaches the reducer
// applyMiddleware - the function that helps you make your replacement for createStore;
// thunk function -  a function that wraps some behavior for execution (e.g. logger)... it's
// let store = applyMiddleware(thunk)(createStore)(reducer);
/*  e.g. let createStoreMW = applyMiddleware(logger, crashReporter)(createStore);
*   todoApp = combineReducers(reducers)
*   let store= createStoreMW(todoApp)
*/

const defaultState = [{ id: 1, time: 5, title: 'DEFAULTSTATE', ticking: false, startTime: 5, paused: true }];
const getStateFromDB = () => get('/api/timer');
const getInitialState = () => {
    return Promise.all([getStateFromDB(), getStateFromLS()]).then(([dbState, lsState]) => {
      console.log('dbState', dbState);
      if (dbState) return dbState;
      console.warn('lsState returned.')
      if (lsState) return lsState;
      console.warn('defaultState returned.')
      return defaultState;
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



// without anon function, I get error 'expected listener to be a function'
store.subscribe(() => storeStateInLS(store.getState()));

// this seems to be called everytime the state changes
const debouncedUpdate = debounce(() => { 
  getStateFromLS().then(currentState => {
      console.log('currentState', currentState);
      currentState.map(timer => fetchPut('/api/timer', timer)
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





