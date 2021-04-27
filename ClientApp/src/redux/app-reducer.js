import { fileApi } from '../API/API.js';


const SET_FILE = "SET_FILE";
const SET_TEXT = "SET_TEXT";

let initialState = {
    text: "",
    title: "",
    key:"",
    decrypted: "dawdawd"
    
    
};
const appReducer = (state = initialState, action) => {
    
    switch (action.type) {
       
        case SET_FILE:

            return {
                
                text: action.data.Text,
                title: action.data.Title,
                key: action.data.Key,
                decrypted: action.data.Decrypted,
               
            };
        case SET_TEXT:
            
            return {
                ...state,
                text: action.text,
                
            };
    
        default: return state;
    };
};


export const setFile = (data) => ({ type: SET_FILE, data});

export const setUploadedFile = (data) => {
    return (dispatch) => {
        
        dispatch(setFile(data));

    };
};
export const postFile = (text, key,title, direction) => {
    return (dispatch) => {
        return fileApi.setFile(text, key,title, direction)
            .then(response => {
               
                //console.log(response);
                dispatch(setFile(response.data));
               

            });

    };
};
export default appReducer;
