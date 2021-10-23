import React from "react";
import axios from "axios";

class BrandModal extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
		  brand: {
        name: "",
        description: "",
        company: {
          companyId: 0,
          name: "",
          rating: {
            description: ""
          }
        },
        categories: []
      }
		};
	}

  componentDidMount() {
    const _this = this;

    if (typeof this.props.id !== "undefined" && this.props.id !== 0) {
      axios.get(`https://localhost:5001/api/Brand/${this.props.id}`).then(function(response) {
      _this.setState({
        brand: response.data
      })
      }).catch((error) => {
        console.log(error);
    })
    }
  }
  
  stars(rating) {
    if (rating >= 4.5)
      return <td className="stars">&#9733;&#9733;&#9733;&#9733;&#9733;</td>
    else if (rating >= 3.5)
      return <td className="stars">&#9733;&#9733;&#9733;&#9733;&#9734;</td>
    else if (rating >= 2.5)
      return <td className="stars">&#9733;&#9733;&#9733;&#9734;&#9734;</td>
    else if (rating >= 1.5)
      return <td className="stars">&#9733;&#9733;&#9734;&#9734;&#9734;</td>
    else if (rating >= 0.5)
      return <td className="stars">&#9733;&#9734;&#9734;&#9734;&#9734;</td>
    else {
      return <td className="stars">&#9734;&#9734;&#9734;&#9734;&#9734;</td>
    }
  }
  
  render() {
    return(
      <div>
        <h1>{this.state.brand.name}</h1>
        <hr></hr>
        <div className="Brand-categories">
          {this.state.brand.categories.map(function (c){
              return <p key={c.categoryId}>{c.name}</p>
            })}
        </div>
        <hr></hr>
        <p>{this.state.brand.description}</p>
        <div className="Company-content">
          <div>
            <h3>Įmonė</h3>
            <p>{this.state.brand.company.name}</p>
            <p>{this.state.brand.company.description}</p>
            <p>{this.state.brand.company.rating.description}</p>
          </div>
          <div>
            <h3>Reitingai</h3>
            <table>
              <tbody>
                <tr>
                  <td>Tvarumas</td>
                  {this.stars(this.state.brand.company.rating.planetRating)}
                  <td>{this.state.brand.company.rating.planetRating}</td>
                </tr>
                <tr>
                  <td>Socialinė gerovė</td>
                  {this.stars(this.state.brand.company.rating.peopleRating)}
                  <td>{this.state.brand.company.rating.peopleRating}</td>
                </tr>
                <tr>
                  <td>Gyvūnų gerovė</td>
                  {this.stars(this.state.brand.company.rating.animalsRating)}
                  <td>{this.state.brand.company.rating.animalsRating}</td>
                </tr>
                <tr>
                  <td>Bendras</td>
                  {this.stars(this.state.brand.company.rating.totalRating)}
                  <td>{this.state.brand.company.rating.totalRating}</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      </div>
    )
  }
}

export default BrandModal;