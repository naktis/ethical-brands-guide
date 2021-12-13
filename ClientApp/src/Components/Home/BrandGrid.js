import React from "react";
import BrandCard from "./BrandCard";

class BrandGrid extends React.Component {
  render() {
    return(
      <div className="result-div">
        { 
          this.props.brands.map(function (brand){
            return (
              <BrandCard 
                brand={brand} 
                key={brand.brandId} 
                sortType={this.props.sortType} 
                user={this.props.user}/>
            )
          }, this) 
        }
      </div>
    )
  }
}

export default BrandGrid;