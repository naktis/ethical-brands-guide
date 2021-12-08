import React from "react";
import BrandCard from "./BrandCard";
import { Link } from "react-router-dom";

class BrandGrid extends React.Component {
  render() {
    return(
      <div className="result-div">
        { 
          this.props.brands.map(function (brand){
            return (
              <Link to={`/view/${brand.brandId}`} className="DecorationNone" key={brand.brandId}>
                <BrandCard brand={brand} key={brand.brandId} sortType={this.props.sortType} />
              </Link>)
          }, this) 
        }
      </div>
    )
  }
}

export default BrandGrid;