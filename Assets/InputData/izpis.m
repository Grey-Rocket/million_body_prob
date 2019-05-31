function izpis(n, min, max, dva)
% 'n' - stevilo teles
% 'min' - najmanjsa amplituda
% 'max' - najvecja amplituda (min je ena)
% filename je test1.txt, spremeni ce zelis

filename="test1.txt";
fid = fopen(filename, "w+");

if (!dva || dva == 0)
  [a,b,c] = generator(min,max,n);
  a(1,:) = [0,0,0];
  b(1,:) = [0,0,0];
  c(1,:) = [0,0,0];
  fprintf(fid,"%d\n",n);
else
  a = zeros(2*n,3);
  b = zeros(2*n,3);
  c = zeros(2*n,3);
  [x,v,f] = generator(min, max, n);
  x(1,:) = [0,0,0];
  v(1,:) = [0,0,0];
  f(1,:) = [0,0,0];
  a(1:n,:) = x(:,:);
  b(1:n,:) = v(:,:);
  c(1:n,:) = f(:,:);
  [x,v,f] = generator(min, max, n);
  x(1,:) = [0,0,0];
  v(1,:) = [0,0,0];
  f(1,:) = [0,0,0];
  a((n+1):end,:) = x(:,:);
  b((n+1):end,:) = v(:,:);
  c((n+1):end,:) = f(:,:);
  
  a(1:n,1) -= (max+min/2);
  a(1:n,2) -= max/2-1;
  b(1:n,1) += sqrt(1000/(2*max+min+1))*10;
  a((n+1):end, 1) += (max+min/2);
  a((n+1):end, 2) += max/2+1;
  b((n+1):end, 1) -= sqrt(1000/(2*max+2*min+1))*10;
  fprintf(fid,"%d\n",2*n);
end

%clf;
%plot3(a(:,1), a(:,2), a(:,3), 'rx');
%hold on;
%plot3(0,0,0, 'kx');
%axis equal;
%return;

for i=1:rows(a)
  fprintf(fid, "%d_", a(i,:));
  fprintf(fid,"\n");
  fprintf(fid, "%d_", b(i,:));
  fprintf(fid,"\n");
  fprintf(fid, "%d_", c(i,:));
  fprintf(fid,"\n");
  if (i == 1 || dva > 0 && i == n+1 || dva && i == n+1)
    fprintf(fid, "%d", 1000);
  else
    fprintf(fid, "%d", 1);
  end
  fprintf(fid,"\n");
end
  
fclose(fid);
"[SYS]: Done"
end