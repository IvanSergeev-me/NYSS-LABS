import React from 'react';
import HomePage from './HomePage.jsx';
import mammoth from "mammoth";
import FileSaver from 'file-saver';
import { connect } from 'react-redux';
import { postFile, setUploadedFile } from '../../redux/app-reducer.js';
import { setCurrentText } from '../../redux/textarea-reducer.js';
import {
    fileOpen,
    directoryOpen,
    fileSave,
    supported,
} from 'browser-fs-access';


class HomePageComponent extends React.Component {
    
    constructor(props) {
        super(props);
    }
    componentDidMount() {
        let cryptType = this.props.match.params.CryptType;
        if (!cryptType) cryptType = "Decrypt";
        
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
    saveFile =()=> {
        
       
     /*
        var blob = new Blob([this.props.appReducer.decrypted], { type: "text/plain;charset=utf-8" });
        const options = {

            extensions: ['.txt', '.docx',],

            fileName: 'aboba.txt'
        };
        const throwIfExistingHandleNotGood = true;
        fileSave(blob, options, null, throwIfExistingHandleNotGood);*/
       // var blob = new Blob([this.props.appReducer.decrypted], { type: 'application/vnd.openxmlformats-officedocument.wordprocessingml.document' });
        //var file = new File([this.props.appReducer.decrypted], "hello world.docx", { type: "application/msword" });
       // FileSaver.saveAs(file);
        let mswordType = 'application/msword';
        let txtType = 'text/plain';
        var data = new Blob([this.props.appReducer.decrypted], { type:mswordType});
        /*var textFile = window.URL.createObjectURL(data);
        if (document.getElementById('download') !== null) {
            document.body.removeChild(document.getElementById('download'));
        }
        var a = document.createElement("a");
        a.setAttribute("id", "download");
        a.setAttribute("href", textFile);
        a.setAttribute("download", "filename");
        a.click();
        document.body.appendChild(a);*/
        const options = { extensions: ['.txt', '.doc']}
        fileSave(data, options);
    
        
        
  
    }
    setInit = (text, key, title, direction) => {
       
        this.props.postFile(text, key,title,direction); 
    }
    render() {
        return (
            <HomePage saveFile={this.saveFile} params={this.props.match.params.CryptType} appReducer={this.props.appReducer} handleFileChange={this.handleFileChange} extractWordRawText={this.extractWordRawText} setInit={this.setInit} />
        );

    }

}
let mapStateToProps = (state) => ({

    appReducer: state.appInit
});

export default connect(mapStateToProps, { postFile, setUploadedFile, setCurrentText})(HomePageComponent);