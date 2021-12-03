import React from "react";

class RequestRow extends React.Component {
  constructor(props){
    super(props);
    this.state = {
      showDescription: false
    }
  }

  render() {
    return(
      <div className="Item-Row" onClick={() => this.setState({ showDescription: !this.state.showDescription })}>
        <div className="Request-name">
          {this.props.children}
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