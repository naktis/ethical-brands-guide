import React from "react";
import { useParams, Link } from 'react-router-dom'
import BrandDetails from "./BrandDetails";
import './ViewPage.css';

function ViewPage() {
  let { id } = useParams();

  return (
		<div className="View-page">
			<div className="Sides">
				<Link to="/"><button type="button">&#8592;</button></Link>
			</div>
			<BrandDetails id={id}/>
			<div className="Sides"></div>
		</div>
	);
}

export default ViewPage;