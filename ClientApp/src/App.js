import './App.css';
import Header from './Components/Header';
import Main from './Components/Main';
import React from "react";
import { Redirect } from 'react-router';

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      loggedIn: false,
      user: {
        username: "",
        id: 0,
        type: "",
        token: ""
      }
    };
  }
  
  handleLogin(user) {
    this.setState({
      loggedIn: true,
      user: {
        username: user.username,
        id: user.userId,
        type: user.type,
        token: user.token
      }
    });
  }

  handleLogout() {
    this.setState({
      loggedIn: false,
      user: {
        username: "",
        id: 0,
        type: "",
        token: ""
      }
    });
  }

  render() {
    return (
      <div className="App">
        <Header 
          user={this.state.user} 
          handleLogout={this.handleLogout.bind(this)} 
        />
        <Main 
          user={this.state.user}
          handleLogin={this.handleLogin.bind(this)} 
        />
        { 
          this.state.loggedIn ? <Redirect to={{
            pathname: '/',
            user: this.state.user
          }}/> 
          : 
          null 
        }
      </div>
    );
  }
}

export default App;
