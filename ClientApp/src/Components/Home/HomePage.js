import React from "react";
import BrandCard from "./BrandCard";
import axios from "axios";
import SelectOption from "./SelectOption";
import { Link } from 'react-router-dom';
import './Home.css';

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
      queryUnsaved: "",
      sortType: "any",
      brandId: 0,
      brandKey: 0,
      brandName: "",
      brandDescription: "",
      brandCategoryId: 0,
      paging: {
        currentPage: 1,
        nextBrands: [],
        buttons: {
          back: {
            state: "disabled",
            class: "Disabled-button"
          },
          next: {
            state: "",
            class: ""
          }
        }
      }
		};

    this.makeRatingCountString = this.makeRatingCountString.bind(this);
    this.fetchData = this.fetchData.bind(this);
    this.getEmptyBrand = this.getEmptyBrand.bind(this);
    this.handleNextButtonEnable = this.handleNextButtonEnable.bind(this);
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

    axios.get("https://localhost:5001/api/Brand?PageNumber=2").then(function(response) {
      let paging = _this.state.paging;
      paging.nextBrands = response.data;
      _this.setState({ paging: paging});
      _this.handleNextButtonEnable(response.data);
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

  handleSearch(query, sortType, categoryId, pageNumber){
    const _this = this;

    axios.get(`https://localhost:5001/api/Brand?PageNumber=${pageNumber}&query=${query}&sortType=${sortType}&categoryId=${categoryId}`)
      .then(function(response) {
        _this.setState({
          brands: response.data,
          query: query,
          sortType: sortType,
          categoryId: categoryId
        });

        if (pageNumber === 1) {
          let paging = _this.state.paging;
          paging.currentPage = 1;
          _this.setState({ paging: paging });
          _this.updatePagingStates(pageNumber+1);
        }
      }).catch((error) => {
        console.log(error);
    })
  }

  handleRatingChange(e) {
    this.handleSearch(this.state.query, e.target.value, this.state.categoryId, this.state.paging.currentPage);
  }

  handleCategoryChange(e) {
    this.handleSearch(this.state.query, this.state.sortType, e.target.value, this.state.paging.currentPage);
  }

  handleSearchClick() {
    this.setState({ query: this.state.queryUnsaved });
    this.handleSearch(this.state.queryUnsaved, this.state.sortType, this.state.categoryId, 1);
  }

 handleQueryChange = (e) => {
    this.setState({ queryUnsaved: e.target.value });
  }

 getEmptyBrand() {
    return { 
      name: "",
      description: "",
      categoryId: 0,
      companyId: 0
    }
  }
/*
  deleteBrand() {
    axios.delete(`https://localhost:44321/api/Brand/${this.state.brandId}`).then(function(response) {
      console.log(`Brand has been deleted`);
      }).catch((error) => {
        console.log(error);
    })
  }
  */

  nextPage() {
    let currentPage = this.state.paging.currentPage;
    this.handleSearch(this.state.query, this.state.sortType, this.state.categoryId, currentPage+1);
    this.updatePagingStates(currentPage+2);

    let paging = this.state.paging;
    
    if(this.state.paging.currentPage === 1) {
      paging.buttons.back.state = "";
      paging.buttons.back.class = "";
    }

    paging.currentPage = currentPage+1;
    this.setState({ paging: paging });
  }

  updatePagingStates(pageNumber) {
    const _this = this;
    axios.get(`https://localhost:5001/api/Brand?PageNumber=${pageNumber}&query=${this.state.query}&sortType=${this.state.sortType}&categoryId=${this.state.categoryId}`)
      .then(function(response) {
        let paging = _this.state.paging;
        paging.nextBrands = response.data;

        _this.setState({ paging: paging})
        _this.handleNextButtonEnable(response.data);
      }).catch((error) => {
        console.log(error);
    })
  }

  handleNextButtonEnable(nextBrands) {
    let paging = this.state.paging;
    console.log(nextBrands);

    if(nextBrands.length === 0 ) {
      paging.buttons.next.state = "disabled";
      paging.buttons.next.class = "Disabled-button";
    } else {
      paging.buttons.next.state = "";
      paging.buttons.next.class = "";
    }

    if(paging.currentPage === 1) {
      paging.buttons.back.state = "disabled";
      paging.buttons.back.class = "Disabled-button";
    }

    this.setState({ paging: paging })
  }

  previousPage() {
    const _this = this;
    let currentPage = this.state.paging.currentPage;

    this.handleSearch(this.state.query, this.state.sortType, this.state.categoryId, currentPage-1);

    let paging = _this.state.paging;
    paging.buttons.next.state = "";
    paging.buttons.next.class = "";
    paging.currentPage = currentPage-1;

    if(paging.currentPage === 1) {
      paging.buttons.back.state = "disabled";
      paging.buttons.back.class = "Disabled-button";
    }
    _this.setState({ paging: paging});
  }

  render() {
    return(
      <main>
        <div id="main-search-div">
          { this.makeRatingCountString() }
          <div id="input-row">
            <input 
              type="text" 
              required 
              minLength="3"
              maxLength="30" 
              placeholder="Įveskite pavadinimą" 
              value={this.state.queryUnsaved} 
              onChange ={this.handleQueryChange.bind(this)}
            >
            </input>
            <img 
              src="/img/search.png" 
              alt="Search icon" 
              onClick={this.handleSearchClick.bind(this)}
            >
            </img>
          </div>
        </div>
        <div id="filter-div">
          <div>
            <h3>Kategorija</h3>
            <select 
              name="category" 
              id="category" 
              value={this.state.categoryId} 
              onChange={this.handleCategoryChange.bind(this)}
            >
              <SelectOption category={{categoryId: 0, name: "Visos"}} />
              { this.state.categories.map(function (category){
                  return <SelectOption category={category} key={category.categoryId}/>
              }) }
            </select>
          </div>
          <div>
            <h3>Rikiavimas pagal reitingą</h3>
            <select 
              name="ratingSort" 
              id="ratingSort" 
              value={this.state.sortType} 
              onChange={this.handleRatingChange.bind(this)}
            >
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
              return (
                <Link to={`/view/${brand.brandId}`} className="DecorationNone" key={brand.brandId}>
                  <BrandCard brand={brand} key={brand.brandId} sortType={this.state.sortType} />
                </Link>)
          }, this) }
        </div>
        <div className="Paging-div">
          <button 
            disabled={this.state.paging.buttons.back.state}
            className={this.state.paging.buttons.back.class}
            onClick={this.previousPage.bind(this)}>
            &#8592;
          </button>
          <div>{this.state.paging.currentPage}</div>
          <button 
            disabled={this.state.paging.buttons.next.state} 
            className={this.state.paging.buttons.next.class}
            onClick={this.nextPage.bind(this)}>
            &#8594;
          </button>
        </div>
      </main>
    )
  }
}

export default HomePage;