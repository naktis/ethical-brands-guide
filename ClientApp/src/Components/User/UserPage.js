import React from "react";
import axios from "axios";
import GenericPage from "../Shared/GenericPage";
import '../Edit/Edit.css';
import ValidationError from "../Shared/Messages/ValidationError";
import ServerError from "../Shared/Messages/ServerError";
import SuccessMessage from "../Shared/Messages/SuccessMessage";
import { Redirect } from "react-router";

class UserPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      //companies: [],
      fields: {
        username: "",
        password: ""
      },
      errors: {
        username: "",
        password: ""
      },
      successMessage: "",
      duplicateMessage: ""
		};

    //this.fetchCompanies = this.fetchCompanies.bind(this);
    //this.handleRatingValidation = this.handleRatingValidation.bind(this);
	}
/*
	componentDidMount() {
    this.fetchCompanies();
	}

  fetchCompanies() {
    const _this = this;
    axios.get("https://localhost:5001/api/Company")
    .then(function(response) {
      _this.setState({
        companies: response.data
      })
    }).catch((error) => {
      console.log(error);
    })
  }
*/
  handleValidation() {
    let formValid = true;

    let errors = this.state.errors;
    errors.username = "";

    if (!this.state.fields.username) {
      formValid = false;
      errors.username = "Įveskite naudotojo vardą";
    } else if (this.state.fields.username < 3) {
      formValid = false;
      errors.username  = "Vardą turi sudaryti daugiau nei 2 simboliai";
    } else if (!this.state.fields.username.match("^[A-Za-z0-9]+$")) {
      formValid = false;
      errors.username = "Vardą gali sudaryti tik raidės ir skaičiai";
    }

    if (!this.state.fields.password) {
      formValid = false;
      errors.password = "Įveskite slaptažodį";
    } else if (this.state.fields.password < 5) {
      formValid = false;
      errors.password  = "Slaptažodį turi sudaryti daugiau nei 5 simboliai";
    } 

    this.setState({ errors: errors });
    return formValid;
  }

  handleSubmit() {
    if (!this.handleValidation())
      return;

    let newUser = this.state.fields;

    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.location.user.token}` }
    }

    console.log(newUser);
    console.log(config);

    axios.post('https://localhost:5001/api/User', newUser, config)
    .then(function (response) {
      _this.setState({ successMessage: "Naudotojas sėkmingai sukurtas" });
      //_this.fetchCompanies();
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
      _this.setState({ duplicateMessage: "Naudotojas tokiu vardu jau egzistuoja" });
    });

    this.setState({ fields: {
      username: "",
      password: ""
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

  handleRatingValidation(rating, field) {
    let errors = this.state.errors;
    let ratingValid = true;

    if (rating === "") {
      errors.rating[field] = "Įveskite įvertinimą";
      ratingValid = false;
    } else if (!Number.isInteger(parseInt(rating))) {
      errors.rating[field] = "Įvertinimas turi būti sveikas skaičius";
      ratingValid = false;
    } else if (rating > 5 || rating < 1) {
      errors.rating[field] = "Įvertinimas turi būti skalėje nuo 1 iki 5.";
      ratingValid = false;
    } else {
      errors.rating[field] = "";
    }

    this.setState({ errors: errors });
    return ratingValid;
  }

  componentWillUnmount() {
    this.setState = (state,callback)=>{
        return;
    };
  }

	render() {
		return(
			<GenericPage>
        { this.props.location.user === undefined || 
          this.props.location.user.type !== "Admin"? 
				<Redirect to="/" /> 
				: 
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
            </div>

            <button className="New-company-button" onClick={this.handleSubmit.bind(this)}>Kurti</button>
            <SuccessMessage>{this.state.successMessage}</SuccessMessage>
            <ServerError>{this.state.duplicateMessage}</ServerError>
      

          </div>
        </div>}
			</GenericPage>
		)
	}
}

export default UserPage;

/*
            <label className="Edit-category-label">Aktyvūs naudotojai</label>
            <div className="Edit-category-list">
              { this.state.companies.map(function (company){
                return <div className="Item-Row" key={company.companyId}>{company.name}</div>
                  }) }
            </div>
*/