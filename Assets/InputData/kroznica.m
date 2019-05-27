function [r,dr,drr] = kroznica(a,t)
  r = [a*sin(t); a*cos(t); rand() - 0.5];
  dr = [a*cos(t); -a*sin(t); rand()];
  drr = -r;
  drr(3,1) = 0;
end