import React from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import { Redirect } from "react-router";

class BrandOptions extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      deleted: false
    };
  }

  handleDelete() {
    if (window.confirm(`Ar tikrai norite ištrinti prekės ženklą ${this.props.brand.name}?`)) {
      const _this = this;

      const config = {
        headers: { Authorization: `Bearer ${this.props.user.token}` }
      }

      axios.delete(`https://localhost:5001/api/Brand/${this.props.brand.brandId}`, config).then(function(response) {
        _this.setState({
          deleted: true
        })
        }).catch((error) => {
          console.log(error);
      })
    }
  }

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
          <div onClick={this.handleDelete.bind(this)}>
						<img 
							src="/img/delete.png" 
							alt="Delete"
						></img>
					</div>
        </div>
        {this.state.deleted ? <Redirect to={'/'}/> : null}
      </div>
    )
  }
}

export default BrandOptions;