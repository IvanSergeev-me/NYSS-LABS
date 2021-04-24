import { fileApi } from '../API/API.js';


const SET_FILE = "SET_FILE";
const SET_TEXT = "SET_TEXT";
//const SET_TEAXTAREA_TEXT = "SET_TEAXTAREA_TEXT";

let initialState = {
    text: "",
    title: "",
    key:"",
    decrypted: ""
    
    
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
        /*case SET_TEAXTAREA_TEXT:

            return {
                ...state,
                currentText: action.text,

            };*/
        default: return state;
    };
};


export const setFile = (data) => ({ type: SET_FILE, data});
//export const setInputText = (text) => ({ type: SET_TEXT, text });
//export const setTextareaText = (text) => ({ type: SET_TEAXTAREA_TEXT, text });
export const setUploadedFile = (data) => {
    return (dispatch) => {
        
        dispatch(setFile(data));

    };
};
/*export const setCurrentText = (text) => {
    return (dispatch) => {

        dispatch(setTextareaText(text));

    };
};*/
export const postFile = (text, key) => {
    return (dispatch) => {
        return fileApi.setFile(text, key)
            .then(response => {
               
                //console.log(response);
                dispatch(setFile(response.data));
               

            });

    };
};
export default appReducer;
