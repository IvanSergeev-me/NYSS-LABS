import axios from 'axios';

const instance = axios.create({
   
    baseURL: "https://localhost:44338/Decrypt"
});
export const fileApi = {
    getFile() {
        try {
            
            return instance.get(``);
        }
        catch(e) {
            
        }
    },
    setFile(text) {
        try {
            
            //return instance.post(`/${text}`);
            
            return instance.post(`/`, {"text":text, "title":"a", "key":"b", "decrypted":"v" } );
        }
        catch (e) {
            console.log(e);
        }
    }
    
};



