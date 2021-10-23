import React from "react";
import BrandCard from "./BrandCard";
import Modal from './Modal.js';
import axios from "axios";
import SelectOption from "./SelectOption";
import BrandModal from "./BrandModal";
import BrandForm from "./BrandForm/BrandForm";

class HomePage extends React.Component {
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
      sortType: "any",
      brandId: 0,
      brandKey: 0,
      showEdit: false,
      brandName: "",
      brandDescription: "",
      brandCategoryId: 0
		};
		this.hideBrand = this.hideBrand.bind(this);
    this.handleQueryChange = this.handleQueryChange.bind(this);
    this.handleSearchClick = this.handleSearchClick.bind(this);
    this.handleRatingChange = this.handleRatingChange.bind(this);
    this.handleCategoryChange = this.handleCategoryChange.bind(this);
    this.makeRatingCountString = this.makeRatingCountString.bind(this);
    this.editBrand = this.editBrand.bind(this);
    this.deleteBrand = this.deleteBrand.bind(this);
    this.fetchData = this.fetchData.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.getEmptyBrand = this.getEmptyBrand.bind(this);
	}

  componentDidMount() {
    this._isMounted = true;
    this.fetchData();
  }

  fetchData() {
    const _this = this;

    axios.get("https://localhost:5001/api/Brand/Count").then(function(response) {
      _this.setState({
        brandCount: response.data
      })
      }).catch((error) => {
        console.log(error);
    })

    axios.get("https://localhost:5001/api/Category").then(function(response) {
      _this.setState({
        categories: response.data
        })
      }).catch((error) => {
        console.log(error);
    })

    axios.get("https://localhost:5001/api/Brand").then(function(response) {
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

    if (this.state.brandCount % 10 === 1 && this.state.brandCount % 100 !== 11)
      return <h1>{this.state.brandCount} prekės ženklo reitingas</h1>
    
    if (this.state.brandCount % 10 === 0 || (this.state.brandCount % 100 >= 11 && this.state.brandCount % 100 <= 19))
      return <h1>{this.state.brandCount} prekės ženklų reitingų</h1>

    return <h1>{this.state.brandCount} prekės ženklų reitingai</h1>
  };

  showBrand = (id) => {
		this.setState({ show: true, brandId: id, brandKey: Math.random()});
	};

  showEdit = () => {
    this.setState({ showEdit: true});
  }

  makeBrandModal = (id) => {
    return <BrandModal id={id}/>
  }
	
	hideBrand = () => {
	  this.setState({ show: false });
	};

  hideEdit = () => {
	  this.setState({ showEdit: false });
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

  getEmptyBrand() {
return { 
			name: "",
			description: "",
			categoryId: 0,
			companyId: 0
		}
  }

  editBrand() {
    this.showEdit();
  }

  deleteBrand() {
    axios.delete(`https://localhost:44321/api/Brand/${this.state.brandId}`).then(function(response) {
      console.log(`Brand has been deleted`);
      }).catch((error) => {
        console.log(error);
    })
  }

  handleSubmit(brand) {
    console.log("AUGUSTINA");
    console.log(this.state.brandId);
    axios.put(`https://localhost:44321/api/Brand/${this.state.brandId}`, brand)
		.then(function (response) {
			console.log(response);
		})
		.catch(function (error) {
			console.log(error);
		});
  }

  brandKeeper(brand) {
    this.setState({ 
      brandName: brand.name,
      brandDescription: brand.description,
      brandCategoryId: brand.categoryId
    });
  }

  getLastBrand() {
    return {
      name: this.state.brandName,
      description: this.state.brandDescription,
      categoryId: this.state.brandCategoryId
    }
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
                  return <BrandCard brand={brand} onClick={() => this.showBrand(brand.brandId)} key={brand.brandId}
                  sortType={this.state.sortType} />
          }, this) }
          <Modal show={this.state.show} handleClose={this.hideBrand} 
            title={""} editable={true} editBrand={this.editBrand}
            deleteBrand={this.deleteBrand}>
            <BrandModal id={this.state.brandId} key={this.state.brandKey} brandKeeper={this.brandKeeper}/>
				  </Modal>
          <Modal show={this.state.showEdit} handleClose={this.hideEdit} 
            title={"Prekės ženklo redagavimas"} editable={false} >
            <BrandForm brand={ this.getEmptyBrand() } handleSubmit={this.handleSubmit}/>
				  </Modal>
        </div>
      </main>
    )
  }
}

export default HomePage;