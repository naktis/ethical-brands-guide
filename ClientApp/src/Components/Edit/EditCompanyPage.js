import React from "react";
import axios from "axios";
import GenericPage from "../Shared/GenericPage";
import './Edit.css';
import ValidationError from "../Shared/Messages/ValidationError";
import ServerError from "../Shared/Messages/ServerError";
import SuccessMessage from "../Shared/Messages/SuccessMessage";
import { Redirect } from "react-router";

class EditCompanyPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      companies: [],
      fields: {
        name: "",
        description: "",
        rating: {
          planetRating: "",
          peopleRating: "",
          animalsRating: "",
          description: ""
        }
      },
      errors: {
        name: "",
        description: "",
        rating: {
          planetRating: "",
          peopleRating: "",
          animalsRating: "",
          description: ""
        }
      },
      successMessage: "",
      duplicateMessage: ""
		};

    this.fetchCompanies = this.fetchCompanies.bind(this);
    this.handleRatingValidation = this.handleRatingValidation.bind(this);
	}

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

  handleValidation() {
    let formValid = true;

    // Do not put this into a single if condition, because error message will only appear on the first field
    let planetRatingValid = this.handleRatingValidation(this.state.fields.rating.planetRating, "planetRating");
    let peopleRatingValid = this.handleRatingValidation(this.state.fields.rating.peopleRating, "peopleRating");
    let animalsRatingValid = this.handleRatingValidation(this.state.fields.rating.animalsRating, "animalsRating");

    if (!planetRatingValid || !peopleRatingValid || !animalsRatingValid)
      formValid = false;

    let errors = this.state.errors;
    errors.name = "";

    if (!this.state.fields.name) {
      formValid = false;
      errors.name = "??veskite pavadinim??";
    } else if (this.state.fields.name.length < 3) {
      formValid = false;
      errors.name  = "Pavadinim?? turi sudaryti daugiau nei 2 simboliai";
    }

    this.setState({ errors: errors });
    return formValid;
  }

  handleSubmit() {
    if (!this.handleValidation())
      return;

    let newCompany = this.state.fields;

    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.location.user.token}` }
    }

    axios.post('https://localhost:5001/api/Company', newCompany, config)
    .then(function (response) {
      _this.setState({ successMessage: "??mon?? s??kmingai sukurta" });
      _this.fetchCompanies();
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
      _this.setState({ duplicateMessage: "??mon?? tokiu pavadinimu jau egzistuoja" });
    });

    this.setState({ fields: {
      name: "",
        description: "",
        rating: {
          planetRating: "",
          peopleRating: "",
          animalsRating: "",
          description: ""
        }
    } });
  }

  handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields });
    this.setState({ successMessage: "" });
  }

  handleRatingValidation(rating, field) {
    let errors = this.state.errors;
    let ratingValid = true;

    if (rating === "") {
      errors.rating[field] = "??veskite ??vertinim??";
      ratingValid = false;
    } else if (!Number.isInteger(parseInt(rating))) {
      errors.rating[field] = "??vertinimas turi b??ti sveikas skai??ius";
      ratingValid = false;
    } else if (rating > 5 || rating < 1) {
      errors.rating[field] = "??vertinimas turi b??ti skal??je nuo 1 iki 5.";
      ratingValid = false;
    } else {
      errors.rating[field] = "";
    }

    this.setState({ errors: errors });
    return ratingValid;
  }

  handleRatingChange(field, e) {
    let fields = this.state.fields;

    this.handleRatingValidation(e.target.value, field);

    fields["rating"][field] = e.target.value;
    this.setState({ 
      fields: fields, 
      successMessage: "",
      duplicateMessage: "" 
    });
  }

  handleRatingDescChange(e) {
    let fields = this.state.fields;
    fields.rating.description = e.target.value;
    this.setState({ 
      fields: fields, 
      successMessage: "",
      duplicateMessage: "" 
    });
  }

  componentWillUnmount() {
    this.setState = (state,callback)=>{
        return;
    };
  }

	render() {
		return(
			<GenericPage>
        { this.props.location.user === undefined ? 
				<Redirect to="/" /> 
				: 
        <div>
          <div className="Edit-page">
            <h1>??mon??s</h1>
            <label>Sukurti nauj?? ??mon??</label>

            <div className="New-company">
              <label>Pavadinimas</label>
              <input 
                type="text" 
                value={this.state.fields["name"]}
                onChange={this.handleChange.bind(this, "name")}
                maxLength="50"
              />
              <ValidationError>{this.state.errors["name"]}</ValidationError>
              
              <label>??mon??s apra??ymas</label>
              <textarea 
                value={this.state.fields["description"]}
                onChange={this.handleChange.bind(this, "description")}
                maxLength="500"
              />

              <fieldset>
                <legend>Reitingai</legend>
                <div>
                  <label>Tvarumas</label>
                  <input 
                    type="text" 
                    value={this.state.fields.rating["planetRating"]}
                    onChange={this.handleRatingChange.bind(this, "planetRating")}
                    maxLength="1"
                  />
                </div>
                <ValidationError>{this.state.errors["rating"]["planetRating"]}</ValidationError>

                <div>
                  <label>Socialin?? gerov?? </label>
                  <input 
                    type="text" 
                    value={this.state.fields["rating"]["peopleRating"]}
                    onChange={this.handleRatingChange.bind(this, "peopleRating")}
                    maxLength="1"
                  />
                </div>
                <ValidationError>{this.state.errors["rating"]["peopleRating"]}</ValidationError>

                <div>
                  <label>Gyv??n?? gerov?? </label>
                  <input 
                    type="text" 
                    value={this.state.fields["rating"]["animalsRating"]}
                    onChange={this.handleRatingChange.bind(this, "animalsRating")}
                    maxLength="1"
                  />
                </div>
                <ValidationError>{this.state.errors["rating"]["animalsRating"]}</ValidationError>

                <label>Reiting?? paai??kinimas</label>
                <textarea 
                  value={this.state.fields["rating"]["description"]}
                  onChange={this.handleRatingDescChange.bind(this)}
                  maxLength="500"
                />
              </fieldset>
            </div>
            <button className="New-company-button" onClick={this.handleSubmit.bind(this)}>Kurti</button>
            <SuccessMessage>{this.state.successMessage}</SuccessMessage>
            <ServerError>{this.state.duplicateMessage}</ServerError>
      
            <label className="Edit-category-label">Keisti ??mones</label>
            <div className="Edit-category-list">
              { this.state.companies.map(function (company){
                return <div className="Item-Row" key={company.companyId}>{company.name}</div>
                  }) }
            </div>
          </div>
        </div>}
			</GenericPage>
		)
	}
}

export default EditCompanyPage;