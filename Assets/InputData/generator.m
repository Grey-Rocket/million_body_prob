function [x,v,f]=generator(min,max,n)
  %generira testne primere za problem miljon teles.
  %n - stevilo teles
  %x - lokacija
  %v - hitrosti
  %f - sile
  
  t = linspace(0,2*pi,n);
  
  x = zeros(n,3);
  v = zeros(n,3);
  f = zeros(n,3);
  
  for i=1:length(t)
    a = randi([min max]);
    a = a + rand()*min/2;
    [d, dr, ddr] = kroznica(a,t(i)+rand()*t(i)*a);
    x(i,1:3) = d;
    v(i,1:3) = (dr/norm(dr)) * sqrt(1000/norm(d))/10;
    f(i,1:3) = ddr;
    
  end
  
  x = x;
  v = v;
  f = f;
  
end
