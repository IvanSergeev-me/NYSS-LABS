import React from 'react';
import HomePage from './HomePage.jsx';
import mammoth from "mammoth";
import { FilePicker } from "react-file-picker";
import { connect } from 'react-redux';
import { postFile} from '../../redux/app-reducer.js';
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

    };

    handleFileChange = file => {
       
        /*const reader = new FileReader();
        reader.readAsArrayBuffer(file);
        reader.onload = e => {
            console.log(file);
            this.extractWordRawText(e.target.result);
        };
       
        this.setState({ title: file.name });*/
    };
     setInit = () => {
      this.props.postFile("abbbbboooobbbaaaa");
      
        
        
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
export default connect(mapStateToProps, { postFile })(HomePageComponent);