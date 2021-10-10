import React from "react";

class BrandCard extends React.Component{
    constructor() {
		super();
		
		this.getRating = this.getRating.bind(this);
	}

    getRating = () => {
        return (
            <div>{this.props.rating}</div>
        )
    };

    render() {
        return (
            <div className="Brand-card" onClick={this.props.onClick}>
                <h2>Tekstasddddd</h2>
                { this.getRating() }
                <p>&#9733;&#9734;&#9734;&#9734;&#9734;</p>
            </div>
    )}
}

export default BrandCard;