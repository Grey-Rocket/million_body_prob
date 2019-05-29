function izpis(n, min, max)
% 'n' - stevilo teles
% 'min' - najmanjsa amplituda
% 'max' - najvecja amplituda (min je ena)
% filename je test1.txt, spremeni ce zelis

[a,b,c] = generator(min,max,n);
a(1,:) = [0,0,0];
b(1,:) = [0,0,0];
c(1,:) = [0,0,0];
filename="test1.txt";
fid = fopen(filename, "w+");
fprintf(fid,"%d\n",n);

%clf;
%plot3(a(:,1), a(:,2), a(:,3), 'rx');
%hold on;
%plot3(0,0,0, 'kx');
%axis equal;
%return;

for i=1:n
  fprintf(fid, "%d_", a(i,:));
  fprintf(fid,"\n");
  fprintf(fid, "%d_", b(i,:));
  fprintf(fid,"\n");
  fprintf(fid, "%d_", c(i,:));
  fprintf(fid,"\n");
  if (i == 1)
    fprintf(fid, "%d", 100);
  else
    fprintf(fid, "%d", 1);
  end
  fprintf(fid,"\n");
end
  
fclose(fid);
"[SYS]: Done"
end