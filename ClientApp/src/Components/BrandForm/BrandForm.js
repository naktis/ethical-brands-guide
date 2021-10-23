import React from "react";
import axios from "axios";
import { components } from "react-select";
import { default as ReactSelect } from "react-select";

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
      optionSelected: null
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

  collectData() {
    var categoryIds = [];
    this.state.optionSelected.forEach(o => {
      categoryIds.push(o.value);
    })

    let newBrand = {
      name: this.state.name,
      description: this.state.description,
      companyId: this.state.companyId,
      categoryIds: categoryIds
    }
    this.props.handleSubmit(newBrand);
  }

	render() {
		return(
			<div className="New-brand-form">
        <label>Pavadinimas:</label>
        <input type="text" value={this.state.name} onChange={this.handleNameChange} />
        <label>Aprašymas</label><textarea value={this.state.description} onChange={this.handleDescriptionChange} />
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
        />
        <label>Įmonė</label>
        <select value={this.state.companyId} onChange={this.handleCompanyChange}>
          { this.state.companies.map(function (company){
              return <option value={company.companyId} key={company.companyId}>{company.name}</option>
          }) }
        </select>
        <input type="submit" value="KURTI" onClick={ this.collectData }/>
      </div>
		)
	}
}

export default BrandForm;