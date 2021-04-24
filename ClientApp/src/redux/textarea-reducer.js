


const SET_KEY = "SET_KEY";
const SET_TEXT = "SET_TEXT";


let initialState = {
    currentText: "",
    currentKey:""
};
const textareaReducer = (state = initialState, action) => {

    switch (action.type) {

        case SET_KEY:

            return {

                ...state,
                currentKey: action.data,

            };
        case SET_TEXT:

            return {
                ...state,
                currentText: action.data,

            };
      
        default: return state;
    };
};


export const setKey = (data) => ({ type: SET_KEY, data });
export const setText = (data) => ({ type: SET_TEXT, data });

export const setCurrentText = (data) => {
    return (dispatch) => {
        
        dispatch(setText(data));

    };
};
export const setCurrentKey = (data) => {
    return (dispatch) => {

         dispatch(setKey(data));

    };
};

export default textareaReducer;
