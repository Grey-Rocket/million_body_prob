function [r,dr,drr] = kroznica(a,t)
  r = [a*sin(t); a*cos(t); rand() - 0.5];
  dr = [cos(t); -sin(t); 0];
  drr = -r;
  drr(3,1) = 0;
end