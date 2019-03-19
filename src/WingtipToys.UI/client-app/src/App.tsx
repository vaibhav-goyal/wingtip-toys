import React, { Component } from 'react';
import './App.css';
import ProductListPage from './components/ProductListPage';

class App extends Component {
  render() {
    return (
      <div>
        <header>
          <div className="content-wrapper">
            <div className="float-left">
              <p className="site-title">
                <a>
                  <img src="assets/logo.jpg"></img>
                </a>
              </p>
            </div>
          </div>
        </header>

        <div id="body">
          <ProductListPage></ProductListPage>
          <section className="content-wrapper main-content clear-fix"></section>
        </div>

        <footer>
          <div className="content-wrapper">
            <div className="float-left">
              <p>OrderDynamics Coding Exercise</p>
            </div>
          </div>
        </footer>
      </div>
    );
  }
}

export default App;
