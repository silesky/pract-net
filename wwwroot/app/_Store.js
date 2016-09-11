import { createStore, applyMiddleware } from 'redux';
// stores the state... hydration happens in _Reducer
import { storeStateInLS,getStateFromLS, get } from './util';



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

const defaultState = [{ id: 1, time: 5, title: '', ticking: false, startTime: 5, paused: true }];
const getStateFromDB = () => get('/api/timer');

const getInitialState = () => {
    return Promise.all([getStateFromDB(), getStateFromLS()]).then(([dbState, lsState]) => {
      if (dbState) return dbState;
      if (lsState) return lsState;
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


getInitialState().then((state) => {
  console.log(state);
  store.dispatch({type: 'HYDRATE', data: state});
})


export default store;





