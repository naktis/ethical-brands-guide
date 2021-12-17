import React from "react";
import ValidationError from "../../Shared/Messages/ValidationError";
import ServerError from "../../Shared/Messages/ServerError";
import SuccessMessage from "../../Shared/Messages/SuccessMessage";
import axios from "axios";
import '../../Edit/Edit.css';

class UserForm extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      fields: {
        username: "",
        password: "",
        type: "Registered"
      },
      errors: {
        username: "",
        password: ""
      },
      successMessage: "",
      duplicateMessage: ""
		};
	}

  handleValidation() {
    let formValid = true;

    let errors = this.state.errors;
    errors.username = "";
    errors.password = "";

    if (!this.state.fields.username) {
      formValid = false;
      errors.username = "Įveskite naudotojo vardą";
    } else if (this.state.fields.username.length < 3) {
      formValid = false;
      errors.username  = "Vardą turi sudaryti daugiau nei 2 simboliai";
    } else if (!this.state.fields.username.match("^[A-Za-z0-9]+$")) {
      formValid = false;
      errors.username = "Vardą gali sudaryti tik angliškos abėcėlės raidės ir skaičiai";
    }

    if (!this.state.fields.password) {
      formValid = false;
      errors.password = "Įveskite slaptažodį";
    } else if (this.state.fields.password.length < 5) {
      formValid = false;
      errors.password  = "Slaptažodį turi sudaryti daugiau nei 4 simboliai";
    } 

    this.setState({ errors: errors });
    return formValid;
  }

  handleSubmit() {
    if (!this.handleValidation())
      return;

    console.log(this.handleValidation());

    let newUser = this.state.fields;

    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.token}` }
    }

    axios.post('https://localhost:5001/api/User', newUser, config)
    .then(function (response) {
      _this.setState({ successMessage: "Naudotojas sėkmingai sukurtas" });
      _this.props.refreshUserList();
    })
    .catch(function (error) {
      console.log(error);
      _this.setState({ duplicateMessage: "Naudotojas tokiu vardu jau egzistuoja" });
    });

    this.setState({ fields: {
      username: "",
      password: "",
      type: "Registered"
    }});
  }

  handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields });
    this.setState({ 
      successMessage: "",
      duplicateMessage: ""
     });
  }

  render() {
		return(
        <div>
          <div className="Edit-page">
            <h1>Naudotojai</h1>
            <label>Sukurti naują naudotoją</label>

            <div className="New-company">
              <label>Naudotojo vardas</label>
              <input 
                type="text" 
                value={this.state.fields["username"]}
                onChange={this.handleChange.bind(this, "username")}
                maxLength="20"
              />
              <ValidationError>{this.state.errors["username"]}</ValidationError>
              
              <label>Slaptažodis</label>
              <input
                type="password"
                value={this.state.fields["password"]}
                onChange={this.handleChange.bind(this, "password")}
                maxLength="20"
              />
              <ValidationError>{this.state.errors["password"]}</ValidationError>

              <label>Naudotojo tipas</label>
              <select
                value={this.state.fields["type"]} 
                onChange={this.handleChange.bind(this, "type")} 
              >
                <option defaultValue value="Registered">Registruotas</option>
                <option value="Admin">Administratorius</option>
              </select>
            </div>

            <button className="New-company-button" onClick={this.handleSubmit.bind(this)}>KURTI</button>
            <SuccessMessage>{this.state.successMessage}</SuccessMessage>
            <ServerError>{this.state.duplicateMessage}</ServerError>
          </div>
        </div>
  )}
}

export default UserForm;