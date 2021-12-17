import React from 'react';
import { Switch, Route } from 'react-router-dom';

import HomePage from './Home/HomePage';
import NewBrandPage from './Menu/NewBrand/NewBrandPage';
import ViewPage from './View/ViewPage';
import EditCategoryPage from './Edit/EditCategoryPage';
import EditCompanyPage from './Edit/EditCompanyPage';
import LoginPage from './User/LoginPage';
import UserPage from './Menu/Users/UserPage';
import RequestPage from './Home/RequestPage';
import RequestsPage from './Menu/Requests/RequestsPage';
import EditBrandPage from './Edit/EditBrandPage';
import BrandByRequestPage from './Menu/Requests/BrandByRequestPage';

const Main = (props) => {
  return (
    <Switch> {/* The Switch decides which component to show based on the current URL.*/}
      <Route 
        exact path='/' 
        render={() => <HomePage user={props.user}/>}
      >
      </Route>
      <Route exact path="/create" component={NewBrandPage} />
      <Route exact path="/view/:id" component={ViewPage} />
      <Route exact path="/categories" component={EditCategoryPage} />
      <Route exact path="/companies" component={EditCompanyPage} />
      <Route exact path="/users" component={UserPage} />
      <Route exact path="/requests" component={RequestsPage} />
      <Route exact path="/request" component={RequestPage} />
      <Route exact path="/editBrand" component={EditBrandPage} />
      <Route exact path="/createBrandByRequest" component={BrandByRequestPage} />
      <Route 
        exact path="/login" 
        render={() => <LoginPage handleLogin={props.handleLogin} />}
      />
    </Switch>
  );
}

export default Main;