import { applyMiddleware, combineReducers, createStore } from "redux";

import appReducer from "./app-reducer.js";
import textareaReducer from "./textarea-reducer.js";
import thunkMiddleware from 'redux-thunk';


let reducers = combineReducers({
    
    appInit: appReducer,
    textareaReducer: textareaReducer

});
let store = createStore(reducers, applyMiddleware(thunkMiddleware));

export default store;