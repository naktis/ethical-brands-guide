import React from "react";
import './Shared.css';

class Pager extends React.Component {
  constructor(props) {
		super(props);
		this.state = {
      currentPage: 1,
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
		};
	}

  handleNextPage() {
    let currentPage = this.state.currentPage;
    let nextPage = currentPage + 1;
    let buttons = this.state.buttons;

    this.props.fetch(nextPage);
    
    if(currentPage === 1) {
      buttons.back.state = "";
      buttons.back.class = "";
    }

    this.setState({ 
      currentPage: nextPage,
      buttons: buttons
    });
  };

  handlePreviousPage() {
    let currentPage = this.state.currentPage;
    let previousPage = currentPage - 1;
    let buttons = this.state.buttons;

    this.props.fetch(previousPage);

    buttons.next.state = "";
    buttons.next.class = "";

    if(currentPage === 1) {
      buttons.back.state = "disabled";
      buttons.back.class = "Disabled-button";
    }

    this.setState({ 
      currentPage: previousPage,
      buttons: buttons
    });
  };

  handleNextButtonEnable(nextRequests) {
    let buttons = this.state.buttons;

    if(nextRequests.length === 0 ) {
      buttons.next.state = "disabled";
      buttons.next.class = "Disabled-button";
    } else {
      buttons.next.state = "";
      buttons.next.class = "";
    }

    if(this.state.currentPage === 1) {
      buttons.back.state = "disabled";
      buttons.back.class = "Disabled-button";
    }

    this.setState({ buttons: buttons });
  }
  
  render() {
    return(
      <div className="Paging-div">
      <button 
        disabled={this.state.buttons.back.state}
        className={this.state.buttons.back.class}
        onClick={this.handlePreviousPage.bind(this)}>
        &#8592;
      </button>
      <div className="Current-page-number">{this.state.currentPage}</div>
      <button 
        disabled={this.state.buttons.next.state} 
        className={this.state.buttons.next.class}
        onClick={this.handleNextPage.bind(this)}>
        &#8594;
      </button>
    </div>
    )
  }
}

export default Pager;