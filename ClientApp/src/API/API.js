import axios from 'axios';

const instance = axios.create({
   
    baseURL: "https://localhost:44338/Decrypt",
    headers: {
        'Content-Type': 'application/json'
    }
});
export const fileApi = {
    getFile() {
        try {
            
            return instance.get(``);
        }
        catch(e) {
            
        }
    },
    setFile(text, key,title, direcrion) {
        try {
            
           
            
            return instance.post(`/`, { "text": text, "title": title, "key": key, "decrypted": "", "cryptDirection": direcrion } );
        }
        catch (e) {
            console.log(e);
        }
    }
    
};



