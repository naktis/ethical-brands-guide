import React from "react";
import axios from "axios";
import { components, default as ReactSelect } from "react-select";
import { Redirect, Link  } from 'react-router-dom'

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
      errors: {}
		};

    this.collectData = this.collectData.bind(this);
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
  }

  handleCategoryChange = (selected) => {
    let fields = this.state.fields;
    fields["categories"] = selected;
    this.setState({ fields });
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

    if (!fields["description"]) {
      formValid = false;
      errors["description"] = "Įveskite aprašymą";
    } else if  (fields["description"].length < 10) {
      formValid = false;
      errors["description"] = "Aprašymą turi sudaryti daugiau nei 10 simbolių";
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

    this.props.handleSubmit(fields);
    this.setState({createSucessful: true});

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
        <span style={{ color: "red" }}>{this.state.errors["name"]}</span>

        <label>Aprašymas</label>
        <textarea 
          value={this.state.fields["description"]}
          onChange={this.handleChange.bind(this, "description")}
          maxLength="500"
        />
        <span style={{ color: "red" }}>{this.state.errors["description"]}</span>

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
          <div>Redaguoti įmones</div>
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
        <span style={{ color: "red" }}>{this.state.errors["companyId"]}</span>

        <input type="submit" value="KURTI" onClick={ this.collectData }/>
        {this.state.createSucessful ? <Redirect to="/" /> : <div></div>}
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
