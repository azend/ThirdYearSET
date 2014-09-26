

a = [
	[2, 1, -3, -2, 4, 1, 5],
	[5, -1, 2, 7, -2, 6, -3],
	[2, 4, -1, 1, 5, -3, -2],
	[1, -2, 3, -1, -5, -1, 6],
	[3, 1, 2, -4, 7, 2, 3],
	[4, 5, -1, 3, 9, -3, -5],
	[-9, 1, -3, 5, 4, -2, 8]
];

b = [
	[2],
	[20],
	[-3],
	[2],
	[34],
	[2],
	[4]
];

def print_matrix(matrix)
	matrix.each do |k|
		puts(k.collect_concat { |i| '%.2f' % i } .join(' | '));
	end
end


def matrix_invert(matrix)
# 
# 	0.upto(matrix_dimensions[0] - 1) do |k|
# 		# Find pivot for column k:
# 		i_max = k;
# 		k.upto(matrix_dimensions[0] - 1) do |i|
# 			if (inverse_matrix[k][i]).abs > (inverse_matrix[k][i_max]).abs
# 				i_max = i;
# 			end
# 		end
# 
# 		if inverse_matrix[i_max][k] == 0
# 			raise 'Matrix is singular!'
# 		end
# 
# 		matrix_swap_rows(inverse_matrix, k, i_max);
# 
# 		k.upto(matrix_dimensions[0] - 1) do |i|
# 			# Do for all remaining elements in current row
# 			k.upto(matrix_dimensions[1] - 1) do |j|
# 				puts "inverse_matrix[i][j] = inverse_matrix[#{i}][#{j}] = #{inverse_matrix[i][j]}"
# 				puts "inverse_matrix[k][j] = inverse_matrix[#{k}][#{j}] = #{inverse_matrix[k][j]}"
# 				puts "inverse_matrix[i][k] = inverse_matrix[#{i}][#{k}] = #{inverse_matrix[i][k]}"
# 				puts "inverse_matrix[k][k] = inverse_matrix[#{k}][#{k}] = #{inverse_matrix[k][k]}"
# 				puts ""
# 				#inverse_matrix[i][j] = inverse_matrix[i][j] - inverse_matrix[k][j] * (inverse_matrix[i][k] / inverse_matrix[k][k]);
# 			end
# 			#Fill lower triangular matrix with zeros
# 			inverse_matrix[i][k] = 0;
# 		end
# 		print_matrix(inverse_matrix);
# 	end
	
	#
	# Algorithm below pulled from ruby core lib/matrix.rb with modifications
	#
	matrix_dimensions = matrix_derive_dimensions(matrix);
	a = matrix.dup;
	row_count = matrix_dimensions[0];

	last = row_count - 1

	0.upto(last) do |k|
		i = k
		akk = a[k][k].abs
		(k+1).upto(last) do |j|
			v = a[j][k].abs
			if v > akk
				i = j
				akk = v
			end
		end
		raise 'Matrix is not singular!' if akk == 0
		if i != k
			a[i], a[k] = a[k], a[i]
			matrix[i], matrix[k] = matrix[k], matrix[i]
		end
		akk = a[k][k]

		0.upto(last) do |ii|
			next if ii == k
			q = a[ii][k].quo(akk)
			a[ii][k] = 0

			(k + 1).upto(last) do |j|
				a[ii][j] -= a[k][j] * q
			end
			0.upto(last) do |j|
				matrix[ii][j] -= matrix[k][j] * q
			end
		end

		(k+1).upto(last) do |j|
			a[k][j] = a[k][j].quo(akk)
		end
		0.upto(last) do |j|
			matrix[k][j] = matrix[k][j].quo(akk)
		end
	end

	a;
end

def matrix_multiply(matrix_a, matrix_b)
	result = [];

	matrix_a.each_index do |row_index|
		result_row = []
		matrix_b[row_index].each_index do |col_index|
			dot_product_sum = 0;
			matrix_a[row_index].each_index do |elm_index|
				dot_product_sum += matrix_a[row_index][elm_index] * matrix_b[elm_index][col_index]
			end
			result_row.push(dot_product_sum);
		end	
		result.push(result_row);
	end

	result
end

def matrix_swap_rows(matrix, row_a, row_b)
	buf = matrix[row_a];
	matrix[row_a] = matrix[row_b];
	matrix[row_b] = buf;
end

def matrix_verify(matrix)
	valid = true;

	firstRowLength = matrix[0].length;

	if (firstRowLength)
		matrix.each do |row|
			if row.length != firstRowLength
				valid = false;
			end
		end
	else
		valid = false;
	end

	valid
end

def matrix_derive_dimensions(matrix)
	dimensions = nil

	if matrix
		if matrix.length > 0
			if matrix[0].length > 0
				dimensions = [matrix.length, matrix[0].length]
			end
		end
	end

	dimensions
end

def matrix_generate_identity(dimensions)
	if dimensions[0] != dimensions[1]
		raise RangeError, "Matrix dimensions are not square"
	end

	axisLength = dimensions[0];
	matrix = [];

	axisLength.times do |y|
		row = [];
		axisLength.times do |x|
			if x == y
				row.push(1);
			else
				row.push(0);
			end
		end
		matrix.push(row);
	end

	matrix
end


puts 'Matrix A';
print_matrix(a);

puts ''

puts 'Matrix B';
print_matrix(b);

puts ''

puts 'Performing operation A^-1 * A * X = A^-1 * B'
puts 'Inverting matrix...'
a_inverted = matrix_invert(a);

puts ''

puts 'Performing operation X = A^-1 * B'
puts 'Multiplying matricies...'
x = matrix_multiply(a_inverted, b);

puts ''

puts 'Matrix X'
print_matrix(matrix_multiply(a_inverted, b));
