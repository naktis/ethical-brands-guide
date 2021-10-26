import React from "react";
import axios from "axios";
import { components, default as ReactSelect } from "react-select";
import { Link  } from 'react-router-dom'
import ValidationError from "../Shared/Messages/ValidationError";
import ServerError from "../Shared/Messages/ServerError";
import SuccessMessage from "../Shared/Messages/SuccessMessage";

class BrandForm extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
      companies: [],
      categoryOptions: [],
      createSucessful: false,
      fields: {
        "name": "", 
        "description":"", 
        "companyId": 0, 
        "categories": null
      },
      errors: {},
      duplicateMessage: this.props.duplicateMessage,
      successMessage: this.props.successMessage
		};

    this.collectData = this.collectData.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
	}

  componentDidMount() {
    const _this = this;

    axios.get("https://localhost:5001/api/Category").then(function(response) {
      _this.setCategories(response.data)
      }).catch((error) => {
        console.log(error);
    })

    axios.get("https://localhost:5001/api/Company").then(function(response) {
      _this.setState({
        companies: response.data
      })
      }).catch((error) => {
        console.log(error);
    })
  }

  setCategories(categories) {
    let options = []
    categories.forEach(c => {
      options.push({
        value: c.categoryId,
        label: c.name
      })
    })
    this.setState({categoryOptions: options}); 
  }

  handleChange(field, e) {
    let fields = this.state.fields;
    fields[field] = e.target.value;
    this.setState({ fields });
    this.setState({
      successMessage: "",
      duplicateMessage: ""
    })
  }

  handleCategoryChange = (selected) => {
    let fields = this.state.fields;
    fields["categories"] = selected;
    this.setState({ fields });
    this.setState({
      successMessage: "",
      duplicateMessage: ""
    })
  };

  handleValidation() {
    let formValid = true;
    let errors = {};
    let fields = this.state.fields;

    if (!fields["name"]) {
      formValid = false;
      errors["name"] = "Įveskite pavadinimą";
    } else if (fields["name"].length < 2) {
      formValid = false;
      errors["name"] = "Pavadinimą turi sudaryti daugiau nei 1 simbolis";
    }

    if (fields["companyId"] === 0) {
      formValid = false;
      errors["companyId"] = "Pasirinkite įmonę iš sąrašo";
    }

    this.setState({ errors: errors });
    return formValid;
  }

  collectData(e) {
    e.preventDefault();

    if (!this.handleValidation())
      return;

    let fields = this.state.fields;

    fields["categoryIds"] = [];
    if (fields["categories"] !== null)
      fields["categories"].forEach(o => {
        fields["categoryIds"].push(o.value);
    })
    delete fields["categories"];

    //this.props.handleSubmit(fields);
    this.handleSubmit(fields);
    this.setState({
      createSucessful: true,
      fields: {
        "name": "", 
        "description":"", 
        "companyId": 0, 
        "categories": null
      }
    });
  }

  handleSubmit(brand) {
		const _this = this;
		axios.post('https://localhost:5001/api/Brand', brand)
		.then(function (response) {
			_this.setState({ successMessage: "Prekės ženklas sėkmingai sukurtas"});
			console.log(response);
		})
		.catch(function (error) {
			_this.setState({ duplicateMessage: "Tokia prekės ženklo ir įmonės kombinacija jau egzistuoja"});
			console.log(error);
		});
	}

	render() {
		return(
			<form className="New-brand-form">
        <h1>{this.props.title}</h1>

        <label>Pavadinimas:</label>
        <input 
          type="text" 
          value={this.state.fields["name"]}
          onChange={this.handleChange.bind(this, "name")}
          maxLength="50"
        />
        <ValidationError>{this.state.errors["name"]}</ValidationError>

        <label>Aprašymas</label>
        <textarea 
          value={this.state.fields["description"]}
          onChange={this.handleChange.bind(this, "description")}
          maxLength="500"
        />

        <div className="Form-double-label-div">
          <label>Kategorija</label>
          <div><Link to="/categories">Redaguoti kategorijas</Link></div>
        </div>
        <ReactSelect
          options={this.state.categoryOptions}
          isMulti
          closeMenuOnSelect={false}
          hideSelectedOptions={false}
          components={Categories}
          onChange={this.handleCategoryChange}
          allowSelectAll={true}
          value={this.state.fields["categories"]}
          placeholder="Pasirinkite kategorijas"
        />

        <div className="Form-double-label-div">
          <label>Įmonė</label>
          <div><Link to="/companies">Redaguoti įmones</Link></div>
        </div>
        <select 
          value={this.state.fields["companyId"]} 
          onChange={this.handleChange.bind(this, "companyId")} 
          required="required"
        >
        <option value="0" disabled={true}>Pasirinkite įmonę</option>
          { this.state.companies.map(function (company){
              return <option value={company.companyId} key={company.companyId}>{company.name}</option>
          }) }
        </select>
        <ValidationError>{this.state.errors["companyId"]}</ValidationError>

        <input type="submit" value="KURTI" onClick={ this.collectData }/>
        <SuccessMessage>{this.state.successMessage}</SuccessMessage>
        <ServerError>{this.state.duplicateMessage}</ServerError>
      </form>
		)
	}
}

export default BrandForm;


const Categories = (props) => {
  return (
    <div>
      <components.Option {...props}>
        <input
          type="checkbox"
          checked={props.isSelected}
          onChange={() => null}
        />{" "}
        <label>{props.label}</label>
      </components.Option>
    </div>
  );
};


// {this.state.createSucessful ? <Redirect to="/" /> : <div></div>}