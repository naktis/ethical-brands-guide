import React from "react";
import axios from "axios";
import RequestRow from "./RequestRow";
import Pager from "../../Shared/Pager";

class RequestsList extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      requests: [],
      nextRequests: [],
      key: 0
		};
	}

  componentDidMount() {
    this.fetchRequests(1);
  }

  fetchRequests(page) {
    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.user.token}` }
    };

    axios.get(`https://localhost:5001/api/Request?PageNumber=${page}`, config)
    .then(function(response) {
      _this.setState({ requests: response.data });
    }).catch((error) => {
      console.log(error);
    });

    axios.get(`https://localhost:5001/api/Request?PageNumber=${page + 1}`, config)
    .then(function(response) {
      _this.setState({ nextRequests: response.data });
      
      if (_this.pager !== undefined)
        _this.pager.handleNextButtonEnable(response.data);
      }).catch((error) => {
        console.log(error);
    });
  }

  render() {
    return(
      <div>
        {this.state.requests.length === 0 ? 
          <p>Neįvertintų prekių ženklų nėra</p> 
          :
          <div>
            <div className="Request-page-header">Neįvertinti prekių ženklai</div>
            { this.state.requests.map(function (request){
                return <RequestRow 
                  key={request.requestId} 
                  request={request}
                  user={this.props.user}
                  handleDelete={this.props.handleDelete.bind(this)}
                >
                  <div className="Request-title">
                    <div className="Request-id">#{request.requestId}</div>
                    <div className="Request-name">{request.name}</div>
                  </div>
                </RequestRow>
              }, this)
            }
            <Pager 
              fetch={ this.fetchRequests.bind(this) }
              ref={(p) => this.pager = p}
            />
          </div>
        }
      </div>
    )
  }
}

export default RequestsList;