import React from 'react';
import { Switch, Route } from 'react-router-dom';

import HomePage from './HomePage';
import CreatePage from './Create/CreatePage';
import ViewPage from './View/ViewPage';
import EditPage from './EditPage';
import EditCategoryPage from './Edit/EditCategoryPage';

const Main = () => {
  return (
    <Switch> {/* The Switch decides which component to show based on the current URL.*/}
      <Route exact path='/' component={HomePage}></Route>
      <Route exact path="/create" component={CreatePage} />
      <Route exact path="/view/:id" component={ViewPage} />
      <Route exact path="/edit" component={EditPage} />
      <Route exact path="/categories" component={EditCategoryPage} />
    </Switch>
  );
}

export default Main;