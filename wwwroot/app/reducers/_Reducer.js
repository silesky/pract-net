import { isEmpty, fetchPost, createUuid } from '../util';
import { defaultState, defaultTimer } from '../_Store';

const reducer = function(state = [], action) {
    let _index;
    let _objEl;
    let _individualTimerObjEl;
    const util = {
        getState_replaceElByIndex: (index, el) => {
            return [...state.slice(0, index),
                state[index] = el, ...state.slice(index + 1)
            ];
        },
        getState_removeElByIndex: (index) => {
            return [...state.slice(0, index), ...state.slice(index + 1)];
        },
        getCurrentIndex: () => {
            const foundObj = state.find((el) => el.id === action.id);
            const foundIndex = state.indexOf(foundObj);
            return foundIndex;
        },
        getCurrentObjEl: () => {
            return state.find((el) => el.id === action.id);
        },
        getNextId: () => {
            let nextId;
            let idArr = state.map(el => el.id);
            if (isEmpty(idArr)) {
                nextId = 1;
            } else {
                /* TIL Math.max of an empty array returns negative infinity, which is passes the null check */
                nextId = Math.max(...idArr) + 1;
            }
            return nextId;
        },
    };
    switch (action.type) {
        case 'HYDRATE':
            console.log('store hydrated', action.data);
            return action.data;
        case 'CLEAR':
            return defaultState;
        case 'ADD_TIMER':
            let newState = [...state, defaultTimer];
            return newState;
        case 'SAVE_START_TIMES':
            console.log('saveStartTimes!')
            let stateWithSavedStartTimes = state.map((el) => {
                // if the timer is paused, don't overwrit e the start time
                // if timer has finished, don't overwrite the start time
                // not using ternary bc it's hard to debug in devtools.
                if (!el.ticking && el.time !== 0) {
                    el.startTime = el.time;
                }
                return el;
            });
            return stateWithSavedStartTimes;

        case 'PAUSE_ALL':
            return state.map((el) => {
                el.paused = true;
                return el;
            });

        case 'SET_TICKING_FALSE_ALL':
            return state.map((el) => {
                el.ticking = false;
                return el;
            });
        case 'RESET':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.time = _individualTimerObjEl.startTime;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'RESET_ALL':
            let stateWithAllTimesReset = state.map((el) => {
                el.time = el.startTime;
                return el;
            });
            return stateWithAllTimesReset;
        case 'SET_TIME':
            _individualTimerObjEl = util.getCurrentObjEl();
            // otherwise input will get parsed as string
            let int = +action.time;
            _individualTimerObjEl.time = int;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'TOGGLE_PAUSEPLAY':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.paused = !_individualTimerObjEl.paused;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'SET_TICKING_TRUE':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.ticking = true;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'SET_TICKING_FALSE':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.ticking = false;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'SET_PAUSE_TRUE':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.paused = true;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'SET_PAUSE_FALSE':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.paused = false;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
        case 'SET_TITLE':
            _individualTimerObjEl = util.getCurrentObjEl();
            _individualTimerObjEl.title = action.text;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _individualTimerObjEl);
            // at the moment that add_timer is instantiated, the state only has two timers
        case 'INCREMENT':
            _index = util.getCurrentIndex();
            _objEl = util.getCurrentObjEl();
            _objEl.time = _objEl.time + 60;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _objEl);
        case 'DECREMENT':
            _index = util.getCurrentIndex();
            _objEl = util.getCurrentObjEl();
            _objEl.time = _objEl.time - 1;
            return util.getState_replaceElByIndex(util.getCurrentIndex(), _objEl);
        case 'REMOVE_TIMER':
            return util.getState_removeElByIndex(util.getCurrentIndex());
        default:
            return state;
    }
};
export default reducer;
