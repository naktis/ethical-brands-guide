import React from "react";
import BrandCard from "./BrandCard";
import Modal from './Modal.js';
import axios from "axios";
import SelectOption from "./SelectOption";



class Main extends React.Component {
  _isMounted = false;

  constructor() {
		super();
		this.state = {
		  show: false,
      brandCount: 0,
      categories: [],
      categoryId: 0,
      brands: [],
      query: "",
      sortType: "any"
		};
		this.showBrand = this.showBrand.bind(this);
		this.hideBrand = this.hideBrand.bind(this);
    this.handleQueryChange = this.handleQueryChange.bind(this);
    this.handleSearchClick = this.handleSearchClick.bind(this);
    this.handleRatingChange = this.handleRatingChange.bind(this);
    this.handleCategoryChange = this.handleCategoryChange.bind(this);
    this.makeRatingCountString = this.makeRatingCountString.bind(this);
	}

  componentDidMount() {
    this._isMounted = true;
    const _this = this;

    axios.get("https://localhost:44321/api/Brand/Count").then(function(response) {
      _this.setState({
        brandCount: response.data
      })
      }).catch((error) => {
        console.log(error);
    })

    axios.get("https://localhost:44321/api/Category").then(function(response) {
      _this.setState({
        categories: response.data
        })
      }).catch((error) => {
        console.log(error);
    })

    axios.get("https://localhost:44321/api/Brand").then(function(response) {
      _this.setState({
        brands: response.data
        })
      }).catch((error) => {
        console.log(error);
    })
  }

  componentWillUnmount() {
    this._isMounted = false;
  }

  makeRatingCountString () {
    if (this.state.brandCount === 0)
      return <h1>Prekės ženklų reitingų nėra</h1>

    if (this.state.brandCount % 10 === 1)
      return <h1>{this.state.brandCount} prekės ženklo reitingas</h1>
    
    if (this.state.brandCount % 10 === 0 || (this.state.brandCount % 100 >= 11 && this.state.brandCount % 100 <= 19))
      return <h1>{this.state.brandCount} prekės ženklų reitingų</h1>

    return <h1>{this.state.brandCount} prekės ženklų reitingai</h1>
  };

  showBrand = () => {
		this.setState({ show: true });
	};
	
	hideBrand = () => {
	this.setState({ show: false });
	};

  handleSearch(query, sortType, categoryId){
    const _this = this;

    axios.get(`https://localhost:44321/api/Brand?query=${query}&sortType=${sortType}&categoryId=${categoryId}`)
      .then(function(response) {
      _this.setState({
        brands: response.data,
        query: query,
        sortType: sortType,
        categoryId: categoryId
        })
      }).catch((error) => {
        console.log(error);
    })
    
  }

  handleRatingChange(e) {
    this.handleSearch(this.state.query, e.target.value, this.state.categoryId);
  }

  handleCategoryChange(e) {
    this.handleSearch(this.state.query, this.state.sortType, e.target.value);
  }

  handleSearchClick() {
    this.handleSearch(this.state.query, this.state.sortType, this.state.categoryId);
  }

 handleQueryChange = (e) => {
    this.setState({ query: e.target.value });
  }

  render() {
    return(
      <main>
        <div id="main-search-div">
          { this.makeRatingCountString() }
          <div id="input-row">
            <input type="text" id="query" name="query" required minLength="3" 
            maxLength="30" placeholder="Įveskite pavadinimą" value={this.state.query} onChange ={this.handleQueryChange}></input>
            <img src="/img/search.png" alt="Search icon" onClick={this.handleSearchClick}></img>
          </div>
        </div>
        <div id="filter-div">
          <div>
            <h3>Kategorija</h3>
            <select name="category" id="category" value={this.state.categoryId} onChange={this.handleCategoryChange}>
              <SelectOption category={{categoryId: 0, name: "Visos"}} />
              { this.state.categories.map(function (category){
                  return <SelectOption category={category} key={category.categoryId}/>
              }) }
            </select>
          </div>
          <div>
            <h3>Rikiavimas pagal reitingą</h3>
            <select name="ratingSort" id="ratingSort" value={this.state.sortType} onChange={this.handleRatingChange}>
              <option value="any">Nėra</option>
              <option value="total">Bendras</option>
              <option value="planet">Tvarumas</option>
              <option value="people">Socialinė gerovė</option>
              <option value="animals">Gyvūnų gerovė</option>
            </select>
          </div>
        </div>
        <div id="result-div">
          { this.state.brands.map(function (brand){
                  return <BrandCard brand={brand} onClick={this.showBrand} key={brand.brandId}
                  sortType={this.state.sortType} />
          }, this) }
          <Modal show={this.state.show} handleClose={this.hideBrand} title="Brando pavadinimas">
					  <p>Jonas joja ir dainuoja</p>
				  </Modal>
        </div>
      </main>
    )
  }
}

export default Main;