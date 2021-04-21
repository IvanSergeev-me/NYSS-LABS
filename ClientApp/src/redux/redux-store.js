import { applyMiddleware, combineReducers, createStore } from "redux";

import appReducer from "./app-reducer";
import thunkMiddleware from 'redux-thunk';


let reducers = combineReducers({
    
    appInit: appReducer

});
let store = createStore(reducers, applyMiddleware(thunkMiddleware));

export default store;