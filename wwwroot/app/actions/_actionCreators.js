import { 
    nextInLine,
    findIdWhereTrue,
    everythingIsPaused,
    fetchPost,
    fetchDelete,
    defaultTimer
  } from '../util.js';


export const _saveStartTimes = () => ({ type: 'SAVE_START_TIMES' });
export const _setTickingTrue = (id) => ({ type: 'SET_TICKING_TRUE', id });
export const _setTickingFalse = (id) => ({ type: 'SET_TICKING_FALSE', id });
export const _setPausedTrue = (id) => ({ type: 'SET_PAUSE_TRUE', id });
export const _setPausedFalse = (id) => ({ type: 'SET_PAUSE_FALSE', id });
// export const removeTimer = (id) => ({ type: 'REMOVE_TIMER', id });
export const removeTimer = (id) => {
  return (dispatch, getState) => {
    console.warn(id);
    dispatch({ type: 'REMOVE_TIMER', id });
    fetchDelete(id)
      .then(r => console.log(r))
      .catch(err => console.error(err));
  };
};


export const reset = (id) => ({ type: 'RESET', id });
export const addTimer = () =>  {
  return (dispatch, getState) => {

    const newTimer = defaultTimer(getState());
    console.log(newTimer);
    dispatch({ type: 'ADD_TIMER', data: newTimer });
    fetchPost("/api/timer", newTimer).then((res, err) => { if (err) throw Error });
  };
};
export const increment = (id) => ({ type: 'INCREMENT', id });
export const decrement = (id) => ({ type: 'DECREMENT', id });
export const setTitle = (text, id) => ({ type: 'SET_TITLE', text, id });
export const pausedPlay = () => {
  
  return (dispatch, getState) => {
    clearInterval(window.myInt);  
    dispatch({ 
      type: 'TOGGLE_PAUSEPLAY',
      id: findIdWhereTrue(getState(), 'ticking'),
    });
  };
};
// paused and reset all
export const resetAll = () =>  {
  return (dispatch) => {
    clearInterval(window.myInt);
    dispatch({ type: 'RESET_ALL'});
  };
};
export const startTicking = (id) => {
  return (dispatch, getState) => { 
    const objectWithMatchingId = () => getState().find((el) => el.id === id);
    // checking if object exusts... it only starts the timer if there are timers on the board.
    if (objectWithMatchingId() && everythingIsPaused(getState())) {
      // if nothing is ticking, save the startTime (so I can later hit reset)...
      dispatch(_saveStartTimes());
      dispatch(_setTickingTrue(id));
      dispatch(_setPausedFalse(id)); 

      window.myInt = setInterval(() => {
        let currentTime = getState().find((el) => el.id === id).time;
        /* based on the current id, find the current time. as long as current time is
        more than zero, keep counting down. */
        if (currentTime > 0 && objectWithMatchingId()) {
          dispatch(decrement(id));
        } 
        else {
          clearInterval(window.myInt);
          dispatch(_setPausedTrue(id));
          dispatch(_setTickingFalse(id));
          dispatch(_setPausedTrue(id)); 
          dispatch(startTicking(nextInLine(getState(), id)));
        }
      }, 1000);
    }
    // ^ called every 1 second
  };

};

