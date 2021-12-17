import React from "react";
import axios from "axios";
import GenericPage from "../Shared/GenericPage";
import ValidationError from "../Shared/Messages/ValidationError";
import ServerError from "../Shared/Messages/ServerError";
import SuccessMessage from "../Shared/Messages/SuccessMessage";

class RequestPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      fields: {
        "name": "", 
        "email": "",
        "description": ""
      },
      errors: {},
			serverErrorMessage: "",
      successMessage: ""
		};
	}

	handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields });
		this.setState({
      serverErrorMessage: "",
      successMessage: ""
    })
  }

  handleValidation() {
		let formValid = true;
    let errors = {};
    let fields = this.state.fields;

		if (!fields["name"]) {
      formValid = false;
      errors["name"] = "Įveskite pavadinimą";
    } else if (fields["name"].length < 2) {
      formValid = false;
      errors["name"] = "Pavadinimą turi sudaryti mažiausiai 2 simboliai";
    }

    if (fields["email"]) {
      if (fields["email"].length < 5 || !/^\S+@\S+\.\S+$/.test(fields["email"])) 
      {
        formValid = false;
        errors["email"] = "Įveskite egzistuojantį el. pašto adresą";
        console.log("buvo blkogai");
      }
    }

		this.setState({ errors: errors });
    return formValid;
	}

  collectData(e) {
		e.preventDefault();

		if (!this.handleValidation())
      return;

		this.postRequest();
	}

  postRequest() {
		const _this = this;

		axios.post('https://localhost:5001/api/Request', this.state.fields)
		.then(function (response) {
			_this.setState({ 
        successMessage: "Prekės ženklo pasiūlymas gautas",
        fields: {
          name: "",
          email: "",
          description: ""
        },
        errors: {}
      })
      console.log(response);
		})
		.catch(function (error) {
			_this.setState({ serverErrorMessage: "Įvyko sistemos klaida. Prašome bandyti vėliau."});
			console.log(error);
		});
	}

  render() {
    return(
      <GenericPage>
        <div id="Request-form">
          <h2>Prekės ženklo pasiūlymas</h2>

          <label>Prekės ženklo pavadinimas</label>
          <input 
            type="text"
            value={this.state.fields["name"]}
            onChange={this.handleChange.bind(this, "name")}
            maxLength="50"
          />
          <ValidationError>{this.state.errors["name"]}</ValidationError>

          <div className="Label-div">
            <label>Jūsų el. pašto adresas</label>
            <div className="Optional-text">(Pasirinktinai)</div>
          </div>
          <div className="Additional-info">Informuosime, jeigu prekės ženklas bus įvertintas</div>
          <input 
            type="text"
            value={this.state.fields["email"]}
            onChange={this.handleChange.bind(this, "email")}
            maxLength="50"
          />
          <ValidationError>{this.state.errors["email"]}</ValidationError>

          <div className="Label-div">
            <label>Aprašymas</label>
            <div className="Optional-text">(Pasirinktinai)</div>
          </div>
          <div className="Additional-info">Įmonės pavadinimas ar bet kokia kita informacija, galinti padėti mums greičiau įvertinti prekės ženklą</div>
          <textarea 
            value={this.state.fields["description"]}
            onChange={this.handleChange.bind(this, "description")}
            maxLength="500"
          />

          <input type="submit" value="SIŪLYTI" onClick={ this.collectData.bind(this) }/>

          <ServerError>{this.state.serverErrorMessage}</ServerError>
          <SuccessMessage>{this.state.successMessage}</SuccessMessage>
        </div>
      </GenericPage>
    )
  }
}

export default RequestPage;