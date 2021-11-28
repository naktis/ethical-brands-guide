import React from "react";
import axios from "axios";
import RequestRow from "./RequestRow";

class RequestsList extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      requests: [],
      paging: {
        currentPage: 1,
        nextRequests: [],
        buttons: {
          back: {
            state: "disabled",
            class: "Disabled-button"
          },
          next: {
            state: "",
            class: ""
          }
        }
      }
		};
	}

  componentDidMount() {
    this.fetchRequests(1);
  }

  fetchRequests(page) {
    const _this = this;
    const config = {
      headers: { Authorization: `Bearer ${this.props.token}` }
    }

    axios.get(`https://localhost:5001/api/Request?PageNumber=${page}`, config)
    .then(function(response) {
      _this.setState({
        requests: response.data
      })
    }).catch((error) => {
      console.log(error);
    });

    axios.get(`https://localhost:5001/api/Request?PageNumber=${page + 1}`, config)
    .then(function(response) {
      let paging = _this.state.paging;
      paging.nextRequests = response.data;
      _this.setState({ paging: paging});
      _this.handleNextButtonEnable(response.data);
      }).catch((error) => {
        console.log(error);
    });
  }

  handleNextButtonEnable(nextRequests) {
    let paging = this.state.paging;

    if(nextRequests.length === 0 ) {
      paging.buttons.next.state = "disabled";
      paging.buttons.next.class = "Disabled-button";
    } else {
      paging.buttons.next.state = "";
      paging.buttons.next.class = "";
    }

    if(paging.currentPage === 1) {
      paging.buttons.back.state = "disabled";
      paging.buttons.back.class = "Disabled-button";
    }

    this.setState({ paging: paging })
  }

  nextPage() {
    let currentPage = this.state.paging.currentPage;
    this.fetchRequests(currentPage+1);

    let paging = this.state.paging;
    
    if(this.state.paging.currentPage === 1) {
      paging.buttons.back.state = "";
      paging.buttons.back.class = "";
    }

    paging.currentPage = currentPage+1;
    this.setState({ paging: paging });
  }

  previousPage() {
    let currentPage = this.state.paging.currentPage;

    this.fetchRequests(currentPage - 1);

    let paging = this.state.paging;
    paging.buttons.next.state = "";
    paging.buttons.next.class = "";
    paging.currentPage = currentPage-1;

    if(paging.currentPage === 1) {
      paging.buttons.back.state = "disabled";
      paging.buttons.back.class = "Disabled-button";
    }
    this.setState({ paging: paging});
  }

  render() {
    return(
      <div>
        {this.state.requests.length === 0 ? <p>Neįvertintų prekių ženklų nėra</p> :
          <div>
            <div className="Request-page-header">Neįvertinti prekių ženklai</div>
            { this.state.requests.map(function (request){
                return <RequestRow key={request.requestId} request={request}>{request.name}</RequestRow>
              })
            }
            <div className="Paging-div">
              <button 
                disabled={this.state.paging.buttons.back.state}
                className={this.state.paging.buttons.back.class}
                onClick={this.previousPage.bind(this)}>
                &#8592;
              </button>
              <div>{this.state.paging.currentPage}</div>
              <button 
                disabled={this.state.paging.buttons.next.state} 
                className={this.state.paging.buttons.next.class}
                onClick={this.nextPage.bind(this)}>
                &#8594;
              </button>
            </div>
          </div>
        }
      </div>
    )
  }
}

export default RequestsList;