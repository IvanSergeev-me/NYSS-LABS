import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { connect } from 'react-redux';

import './custom.css'
import EncryptForm from './components/HomePage/CryptForm/EncryptForm';

 class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/Encrypt' component={EncryptForm} />
      </Layout>
    );
  }
}
let mapStateToProps = (state) => ({
    
});
export default connect(mapStateToProps, { })(App);
