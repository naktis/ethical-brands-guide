import React from "react";
import BrandCard from "./BrandCard";
import Modal from './Modal.js';
import axios from "axios";

class Main extends React.Component {
  constructor() {
		super();
		this.state = {
		  show: false,
      brandCount: 0
		};
		this.showBrand = this.showBrand.bind(this);
		this.hideBrand = this.hideBrand.bind(this);
    this.makeRatingCountString = this.makeRatingCountString.bind(this);
	}

  componentDidMount() {
    const _this = this;

    axios.get("https://localhost:44321/api/Brand/Count").then(function(response) {
      _this.setState({
        brandCount: response.data
      })
  }).catch((error) => {
      console.log(error);
    })
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

  render() {
    return(
      <main>
        <div id="main-search-div">
          { this.makeRatingCountString() }
          <div id="input-row">
            <input type="text" id="query" name="query" required minLength="3" maxLength="30" placeholder="Įveskite pavadinimą"></input>
            <img src="/img/search.png" alt="Search icon"></img>
          </div>
        </div>
        <div id="filter-div">
          <div>
            <h3>Kategorija</h3>
            <select name="category" id="category">
              <option value="none">Bet kuri</option>
              <option value="volvo">Volvo</option>
              <option value="saab">Saab</option>
              <option value="mercedes">Mercedes</option>
              <option value="audi">Audi</option>
            </select>
          </div>
          <div>
            <h3>Rūšiavimas pagal reitingą</h3>
            <select name="ratingSort" id="ratingSort">
              <option value="any">Nėra</option>
              <option value="total">Bendras</option>
              <option value="planet">Ekologija</option>
              <option value="people">Darbuotojų gerovė</option>
              <option value="animal">Gyvūnų gerovė</option>
            </select>
          </div>
        </div>
        <div id="result-div">
          <BrandCard onClick={this.showBrand} rating={"any"}/>
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <BrandCard />
          <Modal show={this.state.show} handleClose={this.hideBrand} title="Brando pavadinimas">
					  <p>Jonas joja ir dainuoja</p>
				  </Modal>
        </div>
      </main>
    )
  }
}

export default Main;