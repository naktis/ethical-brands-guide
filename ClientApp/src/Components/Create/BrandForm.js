import React from "react";
import axios from "axios";
import { components, default as ReactSelect } from "react-select";
import { Redirect } from 'react-router-dom'
import FormValid from "./FormValid";

const Option = (props) => {
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

class BrandForm extends React.Component {
	constructor(props) {
		super(props);
		this.state = {
      categories: [],
      companies: [],
      name: this.props.brand.name,
      description: this.props.brand.description,
      companyId: this.props.brand.companyId,
      categoryOptions: [],
      optionSelected: null,
      createSucessful: false
		};

    this.handleCategoryChange = this.handleCategoryChange.bind(this);
    this.handleCompanyChange = this.handleCompanyChange.bind(this);
    this.collectData = this.collectData.bind(this);
    this.handleNameChange = this.handleNameChange.bind(this);
    this.handleDescriptionChange = this.handleDescriptionChange.bind(this);
	}

  componentDidMount() {
    const _this = this;
    //_this.state.brand = _this.props.brand;

    axios.get("https://localhost:5001/api/Category").then(function(response) {
      _this.setState({
        categories: response.data
      })
      _this.setOptions(response.data)
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

  setOptions(categories) {
    let options = []
    categories.forEach(c => {
      options.push({
        value: c.categoryId,
        label: c.name
      })
    })
    this.setState({categoryOptions: options}); 
  }

  handleCategoryChange(event) {    
    this.setState({categoryId: event.target.value});  
  }

  handleCompanyChange(event) {    
    this.setState({companyId: event.target.value});  
  }

  handleNameChange(event) {    
    this.setState({
      name: event.target.value
    });  
  }

  handleDescriptionChange(event) {    
    this.setState({
      description: event.target.value
    });  
  }

  handleOptionsChange = (selected) => {
    this.setState({
      optionSelected: selected
    });
  };

  collectData(e) {
    e.preventDefault();
    let categoryIds = [];

    if (this.state.optionSelected !== null)
      this.state.optionSelected.forEach(o => {
        categoryIds.push(o.value);
      })

    let newBrand = {
      name: this.state.name,
      description: this.state.description,
      companyId: this.state.companyId,
      categoryIds: categoryIds
    }

    if (FormValid(newBrand)) {
      this.props.handleSubmit(newBrand);
      this.setState({createSucessful: true});
    }
  }

	render() {
		return(
			<form className="New-brand-form">
        <h1>{this.props.title}</h1>
        <label>Pavadinimas:</label>
        <input type="text" value={this.state.name} onChange={this.handleNameChange} required minLength="2" maxLength="40"/>
        <label>Aprašymas</label><textarea value={this.state.description} onChange={this.handleDescriptionChange} required minLength="5" maxLength="200"/>
        <label>Kategorija</label>
        <ReactSelect
          options={this.state.categoryOptions}
          isMulti
          closeMenuOnSelect={false}
          hideSelectedOptions={false}
          components={{
            Option
          }}
          onChange={this.handleOptionsChange}
          allowSelectAll={true}
          value={this.state.optionSelected}
          placeholder="Pasirinkite kategoriją"
        />
        <label>Įmonė</label>
        <select value={this.state.companyId} onChange={this.handleCompanyChange} required="required">
        <option value="0" disabled={true}>Pasirinkite įmonę</option>
          { this.state.companies.map(function (company){
              return <option value={company.companyId} key={company.companyId}>{company.name}</option>
          }) }
        </select>
        <input type="submit" value="KURTI" onClick={ this.collectData }/>
        {this.state.createSucessful ? <Redirect to="/" /> : <div></div>}
      </form>
		)
	}
}

export default BrandForm;