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
    extractWordRawText = arrayBuffer => {

        /*mammoth.extractRawText({ arrayBuffer })
            .then(result => {
                const text = result.value; // The raw text
                const messages = result.messages; // Please handle messages
                this.setState({ text });
            })
            .done();*/
        
        //console.log( String.fromCharCode.apply(null, new Uint8Array(arrayBuffer)));
        
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
                    this.isFilePicked = true;
                    this.props.setCurrentText(bytes);
                };
               
                //this.extractWordRawText(bytes);
            };
        };
        
       
    };
    setInit = (text, key) => {
       // alert(text + "eeeee" +key);
        this.props.postFile(text, key);
      
        
        
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