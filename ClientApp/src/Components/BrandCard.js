import React from "react";

class BrandCard extends React.Component{
    constructor() {
		super();
		
		this.getRating = this.getRating.bind(this);
	}

    getRating = () => {
        let sortType = this.props.sortType;

        var ratingName;
        let rating;

        if (sortType === "animals") {
            ratingName = "Gyvūnų gerovė";
            rating = this.props.brand.ratingAnimals;
        }
        else if (sortType === "planet") {
            ratingName = "Tvarumas";
            rating = this.props.brand.ratingPlanet;
        }
        else if (sortType === "people") {
            ratingName = "Socialinė gerovė"
            rating = this.props.brand.ratingPeople;
        }
        else {
            ratingName = "Bendras reitingas"
            rating = this.props.brand.ratingTotal;
        }

        if (rating >= 4.5)
            return <div><div className="stars">&#9733;&#9733;&#9733;&#9733;&#9733;</div><div>{ratingName}</div></div>
        else if (rating >= 3.5)
            return <div><div className="stars">&#9733;&#9733;&#9733;&#9733;&#9734;</div><div>{ratingName}</div></div>
        else if (rating >= 2.5)
            return <div><div className="stars">&#9733;&#9733;&#9733;&#9734;&#9734;</div><div>{ratingName}</div></div>
        else if (rating >= 1.5)
            return <div><div className="stars">&#9733;&#9733;&#9734;&#9734;&#9734;</div><div>{ratingName}</div></div>
        else if (rating >= 0.5)
            return <div><div className="stars">&#9733;&#9734;&#9734;&#9734;&#9734;</div><div>{ratingName}</div></div>
        else {
            return <div><div className="stars">&#9734;&#9734;&#9734;&#9734;&#9734;</div><div>{ratingName}</div></div>
        }
    };

    render() {
        return (
            <div className="Brand-card" onClick={this.props.onClick}>
                <div className="Brand-name">
                    <h2>{this.props.brand.brandName}</h2>
                </div>
                <h3>{this.props.brand.companyName}</h3>
                { this.getRating() }
            </div>
    )}
}

export default BrandCard;