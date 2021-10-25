import React from "react";
import axios from "axios";
import GenericPage from "../Shared/GenericPage";
import './Edit.css';

class EditCategoryPage extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      categories: [],
      newCategory: "",
      error: ""
		};

    this.fetchCategories = this.fetchCategories.bind(this);
	}

	componentDidMount() {
    this.fetchCategories();
	}

  fetchCategories() {
    const _this = this;
    axios.get("https://localhost:5001/api/Category")
    .then(function(response) {
      _this.setState({
        categories: response.data
      })
    }).catch((error) => {
      console.log(error);
    })
  }

  handleChange(e) {
    this.setState({
      newCategory: e.target.value
    })
  }

  handleValidation() {
    let formValid = true;
    let error = "";

    if (!this.state.newCategory) {
      formValid = false;
      error = "Įveskite pavadinimą";
    } else if (this.state.newCategory.length < 3) {
      formValid = false;
      error = "Pavadinimą turi sudaryti daugiau nei 2 simboliai";
    }

    this.setState({ error: error });
    return formValid;
  }

  handleSubmit() {
    if (!this.handleValidation())
      return;

    let category = {
      name: this.state.newCategory
    }

    const _this = this;
    axios.post('https://localhost:5001/api/Category', category)
    .then(function (response) {
      _this.fetchCategories();
      console.log(response);
    })
    .catch(function (error) {
      console.log(error);
    });

    this.setState({ newCategory: "" });
  }

	render() {
		return(
			<GenericPage>
        <div className="Edit-category-page">
				<h1>Kategorijos</h1>
        <label>Sukurti naują kategoriją</label>
        <div className="New-category">
          <input 
            type="text" 
            value={this.state.newCategory}
            onChange={this.handleChange.bind(this)}
            maxLength="40"
            placeholder="Kategorijos pavadinimas"
          />
          <button onClick={this.handleSubmit.bind(this)}>Sukurti</button>
        </div>
        <span style={{ color: "red" }}>{this.state.error}</span>
  
        <label className="Edit-category-label">Keisti kategorijas</label>
        <div className="Edit-category-list">
          { this.state.categories.map(function (category){
            return <div className="Item-Row" key={category.categoryId}>{category.name}</div>
              }) }
        </div>
        </div>
			</GenericPage>
		)
	}
}

export default EditCategoryPage;