import React from "react";
import { Link } from "react-router-dom";

class BrandOptions extends React.Component {
  render() {
    return(
      <div className="Brand-options">
        <div>
          <Link
            to={{
							pathname: "/editBrand",
							user: this.props.user,
              brand: this.props.brand
						}}
          >
						<img 
							src="/img/edit.png" 
							alt="Edit"
						></img>
					</Link>
        </div>
        <div>
          <Link to="/">
						<img 
							src="/img/delete.png" 
							alt="Delete"
						></img>
					</Link>
        </div>
      </div>
    )
  }
}

export default BrandOptions;