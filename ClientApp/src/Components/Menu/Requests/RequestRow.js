import React from "react";
import { Link } from "react-router-dom";

class RequestRow extends React.Component {
  constructor(props){
    super(props);
    this.state = {
      showDescription: false
    }
  }

  render() {
    return(
      <div className="Item-Row" >
        <div className="Request-row">
          <div className="Request-name" >
            {this.props.children}
          </div>
          <div className="Request-options">
            <img 
              src="/img/more.png" 
              alt="View more"
              className="More-button"
              onClick={() => this.setState({ showDescription: !this.state.showDescription })}
            ></img>
           
            <Link
              to={{
                pathname: "/createBrandByRequest",
                user: this.props.user,
                request: this.props.request
              }}
            >
              <img 
                src="/img/edit.png" 
                alt="Edit"
              ></img>
            </Link>

						<img 
							src="/img/delete.png" 
							alt="Delete"
              onClick={() => this.props.handleDelete(this.props.request)}
						></img>

          </div>
        </div>
        {this.state.showDescription ?
          <div>
            {this.props.request.description === "" ? 
              <div className="Description-header">Aprašymo nėra</div>
              :
              <div>
                <div className="Description-header">Aprašymas</div>
                <div className="Request-description">{this.props.request.description}</div>
              </div>
            }
          </div>
          : 
          null
        }
      </div>
    )
  }
}

export default RequestRow;

             // 