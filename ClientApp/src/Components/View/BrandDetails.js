import React from "react";
import axios from "axios";
import BrandOptions from "./BrandOptions";
import Comment from "./Comment";
import RatingForm from "./RatingForm";

class BrandDetails extends React.Component {
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
      },
      expertRatings: {},
      guestRatings: {},
      comments: [],
      refreshKey: 0
		};
	}

  componentDidMount() {
    const _this = this;

    if (typeof this.props.id !== "undefined" && this.props.id !== 0) {
      axios.get(`https://localhost:5001/api/Brand/${this.props.id}`).then(function(response) {
        _this.setState({
          brand: response.data
        });

        axios.get(`https://localhost:5001/api/Rating/Guest/${response.data.company.companyId}`).then(function(response) {
          _this.setState({
            guestRatings: response.data
          });
          console.log(response.data);
        }).catch((error) => {
          console.log(error);
        });

        axios.get(`https://localhost:5001/api/Rating/Expert/${response.data.company.companyId}`).then(function(response) {
          _this.setState({
            expertRatings: response.data
          });
        }).catch((error) => {
          console.log(error);
        });

        axios.get(`https://localhost:5001/api/Rating/Comments/${response.data.company.companyId}`).then(function(response) {
          _this.setState({
            comments: response.data
          });
        }).catch((error) => {
          console.log(error);
        });
      }).catch((error) => {
        console.log(error);
      });
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

  refreshRatings() {
    window.location.reload();
  }
  
  render() {
    return(
      <div>
        {this.state.brand.name !== "" ?
          <div className="Brand-details">
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
                <h3>ĮMONĖ</h3>
                <p>{this.state.brand.company.name}</p>
                <p>{this.state.brand.company.description}</p>
                <p>{this.state.brand.company.rating.description}</p>
              </div>
            </div>
            <h3 className="More-padding">REITINGAI</h3>
            <div className="Rating-grid">
            <div className="Ratings-div">
              <h3>Bendri</h3>
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
            <div className="Ratings-div">
              <h3>Ekspertų</h3>
              <table>
                <tbody>
                  <tr>
                    <td>Tvarumas</td>
                    {this.stars(this.state.expertRatings.planetRating)}
                    <td>{this.state.expertRatings.planetRating}</td>
                  </tr>
                  <tr>
                    <td>Socialinė gerovė</td>
                    {this.stars(this.state.expertRatings.peopleRating)}
                    <td>{this.state.expertRatings.peopleRating}</td>
                  </tr>
                  <tr>
                    <td>Gyvūnų gerovė</td>
                    {this.stars(this.state.expertRatings.animalsRating)}
                    <td>{this.state.expertRatings.animalsRating}</td>
                  </tr>
                  <tr>
                    <td>Bendras</td>
                    {this.stars(this.state.expertRatings.totalRating)}
                    <td>{this.state.expertRatings.totalRating}</td>
                  </tr>
                </tbody>
              </table>
            </div>
            <div className="Ratings-div">
              <h3>Svečių</h3>
              <table>
                <tbody>
                  <tr>
                    <td>Tvarumas</td>
                    {this.stars(this.state.guestRatings.planetRating)}
                    <td>{this.state.guestRatings.planetRating === 0 ? "nėra" : this.state.guestRatings.planetRating}</td>
                  </tr>
                  <tr>
                    <td>Socialinė gerovė</td>
                    {this.stars(this.state.guestRatings.peopleRating)}
                    <td>{this.state.guestRatings.peopleRating === 0 ? "nėra" : this.state.guestRatings.peopleRating}</td>
                  </tr>
                  <tr>
                    <td>Gyvūnų gerovė</td>
                    {this.stars(this.state.guestRatings.animalsRating)}
                    <td>{this.state.guestRatings.animalsRating === 0 ? "nėra" : this.state.guestRatings.animalsRating}</td>
                  </tr>
                  <tr>
                    <td>Bendras</td>
                    {this.stars(this.state.guestRatings.totalRating)}
                    <td>{this.state.guestRatings.totalRating === 0 ? "nėra" : this.state.guestRatings.totalRating}</td>
                  </tr>
                </tbody>
              </table>
            </div>
            </div>
            <h3 className="More-padding">KOMENTARAI</h3>
            <div className="Comments">
              {this.state.comments.length === 0 ? "Komentarų dar nėra." : 
                <ul>
                { this.state.comments.map(function (comment){
                    return <Comment comment={comment} key={comment.id}/>
                  }) 
                }
                </ul>
              }
            </div>
            <RatingForm companyId={this.state.brand.company.companyId} refreshRatings={this.refreshRatings.bind(this)}/>
            {
              this.props.user === undefined || this.props.user.token === "" ?
              null
              :
              <BrandOptions brand={this.state.brand} user={this.props.user}/>
            }
          </div> :
          <div className="Brand-details loading">
            <p>Prekės ženklas nerastas</p>
          </div>
        }
      </div>
      
    )
  }
}

export default BrandDetails;