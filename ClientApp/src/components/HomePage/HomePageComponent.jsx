import React from 'react';
import HomePage from './HomePage.jsx';
import mammoth from "mammoth";
import { FilePicker } from "react-file-picker";
import { connect } from 'react-redux';
import { postFile, setUploadedFile } from '../../redux/app-reducer.js';
import { setCurrentText } from '../../redux/textarea-reducer.js';

class HomePageComponent extends React.Component {
    
    constructor(props) {
        super(props);
    }
    extractWordRawText = (arrayBuffer,file_name) => {
        //console.log(arrayBuffer);
        mammoth.extractRawText({ arrayBuffer })
            .then(result => {
                 const html = result.value; 
                //console.log(html);
                const messages = result.messages; 
                //console.log(messages);
                this.props.setUploadedFile({ Text: html, Title: file_name, Key: "", Decrypted: "" });
                this.props.setCurrentText(html);
                
            })
            .done(); 
    };
   
    handleFileChange = file => {
       
        const reader = new FileReader();
        let file_type = file.type;
        let file_name = file.name;
        //console.log(file_type);
        if (file_type === "text/plain") {
            
            //reader.readAsArrayBuffer(file, 'CP1251');
            reader.readAsText(file);
           
            reader.onload = e => {
                let bytes = e.target.result;
             
                
                if (!/[А-я]/.test(bytes)) {
                    reader.readAsText(file, 'CP1251');
                    bytes = e.target.result;
                   
                }
                if (bytes) {
                   
                    this.props.setUploadedFile({ Text: bytes, Title: file_name, Key: "", Decrypted: "" });
                    this.props.setCurrentText(bytes);
                };
               
                //this.extractWordRawText(bytes);
            };
        }
        else {
            console.log(file_type);
            reader.readAsArrayBuffer(file);
            reader.onload = e => {
                let bytes = e.target.result;
                this.extractWordRawText(bytes, file_name);
            }
        }
        
       
    };
    setInit = (text, key, title, direction) => {
       
        this.props.postFile(text, key,title,direction);
      
        
        
    }
    render() {
        return (
            <HomePage appReducer={this.props.appReducer} handleFileChange={this.handleFileChange} extractWordRawText={this.extractWordRawText} setInit={this.setInit} />
        );

    }

}
let mapStateToProps = (state) => ({

    appReducer: state.appInit
});

export default connect(mapStateToProps, { postFile, setUploadedFile, setCurrentText})(HomePageComponent);