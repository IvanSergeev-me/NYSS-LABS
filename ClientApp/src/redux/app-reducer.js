import { fileApi } from '../API/API.js';

const SET_INIT = "SET_INIT";
const SET_FILE = "TAKE_FILE";

let initialState = {
    text: "",
    title: "",
    key:"",
    decrypted:""
    
};
const appReducer = (state = initialState, action) => {
    
    switch (action.type) {
        case SET_INIT:
            
            return {
                ...state,
                text: action.text
            };
        case SET_FILE:

            return {
                
                text: action.data.Text,
                title: action.data.Title,
                key: action.data.Key,
                decrypted: action.data.Decrypted,
            };
        default: return state;
    };
};

export const setIniatialised = (text) => ({type:SET_INIT, text});
export const setFile = (data) => ({ type: SET_FILE, data});
/*export const  getFile = () =>{
    return (dispatch) => {
        return fileApi.getFile()
            .then(response => {
                //if (response.data.resultCode === 0) {
                    //console.log(response);
                    dispatch(setIniatialised(response.data));
                //};
                
            });
        
    };
};*/
export const postFile = (text) => {
    return (dispatch) => {
        return fileApi.setFile(text)
            .then(response => {
                //if (response.data.resultCode === 0) {
                console.log(response);
                dispatch(setFile(response.data));
                //};

            });

    };
};
export default appReducer;
