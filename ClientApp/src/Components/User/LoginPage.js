import React from "react";
import axios from "axios";
import GenericPage from "../Shared/GenericPage";
import ValidationError from "../Shared/Messages/ValidationError";
import ServerError from "../Shared/Messages/ServerError";
import "./User.css";

class LoginPage extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
      fields: {
        "username": "", 
        "password":""
      },
      errors: {},
			mismatchMessage: "",
		};
	}

	handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields });
		this.setState({
      mismatchMessage: ""
    })
  }

	handleValidation() {
		let formValid = true;
    let errors = {};
    let fields = this.state.fields;

		if (!fields["username"]) {
      formValid = false;
      errors["username"] = "Enter your user name";
    }

		if (!fields["password"]) {
      formValid = false;
      errors["password"] = "Enter your password";
    }

		this.setState({ errors: errors });
    return formValid;
	}

	tryLogin() {
		const _this = this;
		axios.post('https://localhost:5001/api/User/Login', this.state.fields)
		.then(function (response) {
			_this.props.handleLogin(response.data);
		})
		.catch(function (error) {
			_this.setState({ mismatchMessage: "Username and password do not match."});
			console.log(error);
		});
	}

	collectData(e) {
		e.preventDefault();

		if (!this.handleValidation())
      return;

		this.tryLogin();
	}

	render() {
		return(
			<GenericPage>
        <div className="Login-form">
          <h2>Log in to your account</h2>

          <label>User name</label>
          <input 
            type="text"
            value={this.state.fields["username"]}
            onChange={this.handleChange.bind(this, "username")}
            maxLength="20"
          />
          <ValidationError>{this.state.errors["username"]}</ValidationError>

          <label>Password</label>
          <input 
            type="password"
            value={this.state.fields["password"]}
            onChange={this.handleChange.bind(this, "password")}
            maxLength="20"
          />
          <ValidationError>{this.state.errors["password"]}</ValidationError>

          <input type="submit" value="LOG IN" onClick={ this.collectData.bind(this) }/>
          <ServerError>{this.state.mismatchMessage}</ServerError>
        </div>
      </GenericPage>
		)
	}
}

export default LoginPage;