import React from "react";
import axios from "axios";
import ServerError from "../Shared/Messages/ServerError";
import ValidationError from "../Shared/Messages/ValidationError";
import SuccessMessage from "../Shared/Messages/SuccessMessage";
import '../Edit/Edit.css'

class RatingForm extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      fields: {
        planetRating: "",
        peopleRating: "",
        animalsRating: "",
        comment: "",
        companyId: props.companyId
      },
      errors: {
        planetRating: "",
        peopleRating: "",
        animalsRating: "",
        comment: ""
      },
      successMessage: "",
      duplicateMessage: ""
		};
    this.handleRatingValidation = this.handleRatingValidation.bind(this);
	}

  handleRatingValidation(rating, field) {
    let errors = this.state.errors;
    let ratingValid = true;

    if (rating === "") {
      errors[field] = "Įveskite įvertinimą";
      ratingValid = false;
    } else if (!Number.isInteger(parseInt(rating))) {
      errors[field] = "Įvertinimas turi būti sveikas skaičius";
      ratingValid = false;
    } else if (rating > 5 || rating < 1) {
      errors[field] = "Įvertinimas turi būti skalėje nuo 1 iki 5.";
      ratingValid = false;
    } else {
      errors[field] = "";
    }

    this.setState({ errors: errors });
    return ratingValid;
  }

  handleValidation() {
    let formValid = true;

    // Do not put this into a single if condition, because error message will only appear on the first field
    let planetRatingValid = this.handleRatingValidation(this.state.fields.planetRating, "planetRating");
    let peopleRatingValid = this.handleRatingValidation(this.state.fields.peopleRating, "peopleRating");
    let animalsRatingValid = this.handleRatingValidation(this.state.fields.animalsRating, "animalsRating");

    if (!planetRatingValid || !peopleRatingValid || !animalsRatingValid)
      formValid = false;

    let errors = this.state.errors;
    errors.comment = "";

    if (this.state.fields.comment) {
      if (this.state.fields.comment.length > 500) {
        formValid = false;
        errors.comment = "Komentaras turi būti trumpesnis nei 500 simbolių.";
      }
    }

    this.setState({ errors: errors });
    return formValid;
  }

  handleSubmit() {
    if (!this.handleValidation())
      return;

    console.log(this.handleValidation());

    let newRating = this.state.fields;

    const _this = this;
    axios.post('https://localhost:5001/api/Rating', newRating)
    .then(function () {
      _this.setState({ successMessage: "Reitingas gautas" });
      _this.props.refreshRatings();
    })
    .catch(function (error) {
      console.log(error);
      _this.setState({ duplicateMessage: "Įvyko serverio klaida, prašome bandyti vėliau." });
    });

    this.setState({ fields: {
      planetRating: "",
      peopleRating: "",
      animalsRating: "",
      comment: ""
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

  handleRatingChange(field, e) {
    let fields = this.state.fields;

    this.handleRatingValidation(e.target.value, field);

    fields[field] = e.target.value;
    this.setState({ 
      fields: fields, 
      successMessage: "",
      duplicateMessage: "" 
    });
  }

  handleRatingDescChange(e) {
    let fields = this.state.fields;
    fields.comment = e.target.value;
    this.setState({ 
      fields: fields, 
      successMessage: "",
      duplicateMessage: "" 
    });
  }

  render() {
		return(
        <div>
          <div className="New-rating">
            <h3  className="More-padding">Pateikti naują vertinimą</h3>
            <div className="New-rating-form">
            <div>
                <label>Tvarumas</label>
                <input 
                  type="text" 
                  value={this.state.fields["planetRating"]}
                  onChange={this.handleRatingChange.bind(this, "planetRating")}
                  maxLength="1"
                />
              </div>
              <ValidationError>{this.state.errors["planetRating"]}</ValidationError>

              <div>
                <label>Socialinė gerovė </label>
                <input 
                  type="text" 
                  value={this.state.fields["peopleRating"]}
                  onChange={this.handleRatingChange.bind(this, "peopleRating")}
                  maxLength="1"
                />
              </div>
              <ValidationError>{this.state.errors["peopleRating"]}</ValidationError>

              <div>
                <label>Gyvūnų gerovė </label>
                <input 
                  type="text" 
                  value={this.state.fields["animalsRating"]}
                  onChange={this.handleRatingChange.bind(this, "animalsRating")}
                  maxLength="1"
                />
              </div>
              <ValidationError>{this.state.errors["animalsRating"]}</ValidationError>

              <label>Komentaras</label>
              <textarea 
                value={this.state.fields["comment"]}
                onChange={this.handleRatingDescChange.bind(this)}
                maxLength="500"
              />
            </div>

            <button className="New-company-button" onClick={this.handleSubmit.bind(this)}>SIŲSTI</button>
            <SuccessMessage>{this.state.successMessage}</SuccessMessage>
            <ServerError>{this.state.duplicateMessage}</ServerError>
          </div>
        </div>
  )}
}

export default RatingForm;