import React, { Component } from 'react';
import HomePage from './HomePage/HomePageComponent.jsx';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
        <div>
            <HomePage/>
      </div>
    );
  }
}
