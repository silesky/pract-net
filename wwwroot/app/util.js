export const fetchPost = (route, data) => {
    return fetch(route, {
    method: 'POST',
    headers: new Headers({
      'Content-Type': 'application/json'
    }),
    body: JSON.stringify(data)
  })
}

export const fetchPut = (route, data) => {
    return fetch(route, {
    method: 'PUT',
    headers: new Headers({
      'Content-Type': 'application/json'
    }),
    body: JSON.stringify(data)
  })
}



export const getNextId = (state) => {
  let nextId;
  let idArr = state.map(el => el.id);
  if (isEmpty(idArr)) {
    nextId = 1;
  } else {
    /* TIL Math.max of an empty array returns negative infinity, which is passes the null check */
    nextId = Math.max(...idArr) + 1;
  }
  return nextId;
}
export const getNextOrderNum = (state) => {
  let nextOrderNum;
  const orderArr =  state.map(el => el.order)
  if (isEmpty(orderArr))    {
    nextOrderNum = 1
  }
  else {
    nextOrderNum = Math.max(...orderArr) + 1;
  }
  return nextOrderNum;
}

export const getExistingTimerGroupId = (state) => {
  return state.map(el => el.timerGroupId)[0]
}
// default TimerId sucks
export const defaultTimer = (state) => ({
  order: getNextOrderNum(state),
  id: uuid(),
  timerGroupId: getExistingTimerGroupId(state),  //this guid changes on every seed
  title: 'DEFAULTSTATE',
  ticking: false,
  time: 5,
  startTime: 5,
  paused: true
})

export const fetchDelete = (id) => fetch(`/api/timer/${id}`, {
  method: "DELETE",
  headers: new Headers({
    "Content-Type": "application/json",
  }),
})


export const uuid = () => {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
      let r = Math.random()*16|0, v = c == 'x' ? r : (r&0x3|0x8);
      return v.toString(16);
  });
}
export const get = (filepath) => {
  return new Promise((resolve, reject) => {
  // new request object
    let req = new XMLHttpRequest();
    // req.open(method, filepath, true <-- async by default)
    req.open('GET', filepath);
    // event listener
    req.onload = () => {
      // 4 = request over, responsedata is finished downloading
      if (req.status >= 200) {
        resolve(JSON.parse(req.response));
      }
    }
    req.onerror = () => {
      // gets passed to the error object
      reject({
        statusText: req.statusText
       });
    }
      // query the server and handle the result
    req.send(null);
    });
 }
 


export const storeStateInLS = (obj) => {
  if (typeof obj === 'object' || 'array') {
    // convert to string bc stupid localstorage only supports plain string
    const objString = JSON.stringify(obj);
    localStorage.setItem('storedState', objString);
  }
};

export const getStateFromLS = () => {
  const string = localStorage.getItem('storedState');
  const stateObj = JSON.parse(string);
  if (!stateObj) return Promise.resolve(null);
  

  return Promise.resolve(stateObj);
};

// used in TimerBoxCountDown
export const secondsToMinutesAndHours = (totalSeconds) => {
  let displayString;
  // Math.round also casts the argument to an integer.
  totalSeconds = Math.round(totalSeconds);
  let totalHours = Math.floor(totalSeconds / 3600);
  let totalMinutes = Math.floor(totalSeconds / 60);
  // ...with remaning minutes (e.g 1:05:00...)
  let remainingMins = totalMinutes - ((totalHours * 3600) / 60);
  let remainingSecs = totalSeconds - totalMinutes * 60;
  // if remaining mins/seconds is under 10, add a 0
  let minsWithLeadingZeros = (remainingMins < 10) ? ('0' + remainingMins) : remainingMins;
  let secsWithLeadingZeros = (remainingSecs < 10) ? ('0' + remainingSecs) : remainingSecs;
  // 1hr+
  if (totalSeconds >= 3600) {
    displayString = `${totalHours}:${minsWithLeadingZeros}:${secsWithLeadingZeros}`;
  }
  // from 0-60min
  else if (totalSeconds < 3600 && totalSeconds > 0) {
    displayString = `${remainingMins}:${secsWithLeadingZeros}`;
  }
  // if timer is done
  else if (totalSeconds <= 0) {
    displayString = '0:00';
  }
  return displayString;
};

export const isEmpty = (thing) => (!thing || !thing.length);

export const findIdWhereTrue = (state, prop) => {
    let timerObj = state.find((el) => el[prop] === true);
    let id = timerObj ? timerObj.id : false;
    return id;
};

// expect all timers to have a paused value of true
export const everythingIsPaused = (state) => {
  let arr = state.map((el) => el.paused).filter((el) =>!el);
  return !arr.length; // if array has things, return false
};


export const getTickingId = (state) => {
  let item = state.find((el) => el.ticking).id;
  let id = !isEmpty(item) ? item : false;
  return id;
}; // grab the nextId given the current state. current
export const nextInLine = (state, currentId) => {
  let nextId;
  const idArr = state.map((el) => el.id); // [1, 4, 6]
  if (isEmpty(idArr)) {
    nextId = 1;
  }
  else {
    const currentIndex = idArr.indexOf(currentId); // e.g. if 4 , then zero
    nextId = idArr[currentIndex + 1];
  }
  return nextId;
};
